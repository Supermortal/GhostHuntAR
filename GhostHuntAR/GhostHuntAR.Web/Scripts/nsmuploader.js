(function ($) {

  $.fn.nsmUploader = function (preSubmitCallback, successCallback) {

    var modalDiv = null;
    var modalShown = false;
    var modal = null;
    var progressBar = null;
    var modelDivWidth = null;
    var hasFiles = false;

    var constructor = function() {

      if ($.fn.dialog) {
        modalDiv = $("<div></div>");
        $("body").append(modalDiv);
      }

    };

    var transferComplete = function (event) {
      displayProgress(1);
    };

    var progressHandler = function(event) {
      var percent = 0;
      var position = event.loaded || event.position; /*event.position is deprecated*/
      var total = event.total;
      if (event.lengthComputable) {
        percent = position / total;
        displayProgress(percent);
      }
    };

    var displayProgress = function(progress) {
      
      if (modalDiv && hasFiles == true) {
        createAndShowModal();
        $(progressBar).width((progress * modelDivWidth) * 1);

        if (progress == 1) {
          $(progressBar).css("background-color", "green");
        }
      }

    };

    var createAndShowModal = function() {

      if (modalShown == false) {

        $(modalDiv).unbind();
        $(modalDiv).empty();
        modal = $(modalDiv).dialog({
          modal: true,
          buttons: [
            {
              text: "Ok",
              click: function () {
                modalShown = false;
                $(this).dialog("close");
              }
            }
          ]
        });

        modelDivWidth = $(modalDiv).width();
        var label = $("<p>Upload Progress: </p>");
        $(modalDiv).append(label);
        progressBar = $("<div style='height: 25px; background-color: gray;'></div>");
        $(modalDiv).append(progressBar);

        modalShown = true;
      }

      modal.show();

    };

    var getFormData = function (form) {

      var data = new FormData();

      var inputs = $(form).find("input");
      $(inputs).each(function () {
        if ($(this).prop("type") != "file") {
          data.append($(this).prop("name"), $(this).val());
        }
      });

      var selects = $(form).find("select");
      $(selects).each(function () {
        if ($(this).prop("type") != "file") {
          data.append($(this).prop("name"), $(this).val());
        }
      });

      var textareas = $(form).find("textarea");
      $(textareas).each(function () {
        if ($(this).prop("id") == "xinhaTextArea") {
          data.append($(this).prop("name"), Xinha.getEditor("xinhaTextArea").getEditorContent());
        }
        else {
          data.append($(this).prop("name"), $(this).val());
        }
      });

      var files = $(form).find(":file");
      hasFiles = false;
      $(files).each(function () {
        var file = files[0].files[0];

        if (file) {
          data.append($(this).prop("name"), file);
          hasFiles = true;
        }
      });

      //if (hasFile == false) {
      //  return null;
      //}

      return data;

    };

    var uploadFiles = function (form) {

      if (preSubmitCallback) {
        preSubmitCallback();
      }

      // START A LOADING SPINNER HERE

      // Create a formdata object and add the files
      var data = getFormData(form);

      //try {
      //  if (data == null) {
      //    $(form).submit();
      //    return;
      //  }
      //}
      //catch (ex) {
      //  console.log(ex);
      //}

      $.ajax({
        async: true,
        url: $(form).prop("action"),
        type: 'POST',
        data: data,
        cache: false,
        dataType: 'json',
        processData: false, // Don't process the files
        contentType: false, // Set content type to false as jQuery will tell the server its a query string request
        success: function (data, textStatus, jqXHR) {
          if (successCallback) {
            successCallback(data);
          }
        },
        error: function(jqXHR, textStatus, errorThrown) {
          if (successCallback) {
            successCallback();
          }
        },
        xhr: function(){
          var xhr = jQuery.ajaxSettings.xhr();

          if (xhr.upload) {
            xhr.upload.addEventListener('progress', progressHandler, false);
            xhr.upload.addEventListener("load", transferComplete, false);
          }

          return xhr;
        }
      });
    };

    var submitCallback = function (form, e) {

      e.stopPropagation();
      e.preventDefault();

      uploadFiles(form);

    };

    constructor();
    return this.each(function() {

      if (window.FormData !== undefined) {

        var form = this;
        //$(form).submit(function(e) {
        //  submitCallback(form, e);
        //});

        var submits = $(this).find(":submit");
        $(submits).each(function() {
          $(this).prop("type", "button");
          $(this).click(function(e) {
            submitCallback(form, e);
          });
        });

      }

    });

  };

}(jQuery));