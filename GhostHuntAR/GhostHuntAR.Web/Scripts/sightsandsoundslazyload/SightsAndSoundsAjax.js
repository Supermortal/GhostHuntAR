var SightsAndSoundsAjax = function (argObj) {

  var returnObj = {};

  ////////

  returnObj.scrollBusy = false;
  returnObj.type = null; //{sound, image, text, video}
  returnObj.mode = null; //{user, location}
  returnObj.currentLocationId = null;
  returnObj.currentUserName = null;
  returnObj.scrollContainerId = null;
  returnObj.currentUserId = null;

  /////////////////////////////////////////////////////////////////////////

  returnObj.page = {};

  returnObj.page["sound"] = 1;
  returnObj.page["image"] = 1;
  returnObj.page["text"] = 1;
  returnObj.page["video"] = 1;
  returnObj.page["locations"] = 1;

  returnObj.pageLoaded = {};

  returnObj.pageLoaded["sound"] = {};
  returnObj.pageLoaded["image"] = {};
  returnObj.pageLoaded["text"] = {};
  returnObj.pageLoaded["video"] = {};
  returnObj.pageLoaded["locations"] = {};

  ////////

  returnObj.outOfPages = {};

  returnObj.outOfPages["sound"] = false;
  returnObj.outOfPages["image"] = false;
  returnObj.outOfPages["text"] = false;
  returnObj.outOfPages["video"] = false;
  returnObj.outOfPages["locations"] = false;

  ////////

  returnObj.locationTitles = {};

  returnObj.scrollBusy = false;

  ////////

  returnObj.locationTitles = {};

  ////////////////////////////////////END PROPERTIES/////////////////////////////////////


  ////////////////////////////////////CLICK HANDLERS////////////////////////////////////

  returnObj.clickHandlers = {};

  returnObj.clickHandlers["sound"] = function (obj, element, e, context, titleElement) {
    /*
    Description "Test Description 2"	
    Flags []
    GHLocationID 1
    SoundID 5
    Url null
    UserName "admin"
    */

    var dialog = $("#editDialog");

    var descriptionLabel = $("<span class='margin-right'>Description: </span>");
    $(dialog).append(descriptionLabel);
    var descriptionEdit = $("<input type='text' value='" + obj.Description + "' />");
    $(dialog).append(descriptionEdit);

    $(dialog).dialog({
      dialogClass: 'no-title-bar',
      modal: true,
      close: function(event, ui) {
        dialog.empty();
      },
      buttons: [
        {
          text: "Ok",
          click: function() {

            var self = this;
            var description = $(descriptionEdit).val();

            $.ajax({
              url: context.editUrls[context.type] + "/" + obj.SoundID,
              type: 'POST',
              headers: { 'Content-type': 'application/json' },
              dataType: 'json',
              data: JSON.stringify({ id: obj.SoundID, soundDescription: description, referrerUrl: "AJAX" }),
              context: document.body,
              success: function (data) {
                $(self).dialog("close");
                $(titleElement).html(description);
              },
              error: function (e) {
                console.log(e);
              }
            });

          }
        },
        {
          text: "Cancel",
          click: function() {
            $(this).dialog("close");
          }
        }
      ]
    });

  };

  returnObj.clickHandlers["image"] = function (obj, element, e, context, titleElement) {

    var dialog = $("#editDialog");

    var descriptionLabel = $("<span class='margin-right'>Caption: </span>");
    $(dialog).append(descriptionLabel);
    var descriptionEdit = $("<input type='text' value='" + obj.Caption + "' />");
    $(dialog).append(descriptionEdit);

    $(dialog).dialog({
      dialogClass: 'no-title-bar',
      modal: true,
      close: function (event, ui) {
        dialog.empty();
      },
      buttons: [
        {
          text: "Ok",
          click: function () {

            var self = this;
            var imageCaption = $(descriptionEdit).val();

            $.ajax({
              url: context.editUrls[context.type] + "/" + obj.ImageID,
              type: 'POST',
              headers: { 'Content-type': 'application/json' },
              dataType: 'json',
              data: JSON.stringify({ id: obj.ImageID, imageCaption: imageCaption, referrerUrl: "AJAX" }),
              context: document.body,
              success: function (data) {
                $(self).dialog("close");
                $(titleElement).html(imageCaption);
              },
              error: function (e) {
                console.log(e);
              }
            });

          }
        },
        {
          text: "Cancel",
          click: function () {
            $(this).dialog("close");
          }
        }
      ]
    });

  };

  returnObj.clickHandlers["text"] = function (obj, element, e, context, titleElement) {

    var dialog = $("#editDialog");

    var descriptionLabel = $("<span class='margin-right'>Title: </span>");
    $(dialog).append(descriptionLabel);
    var descriptionEdit = $("<input type='text' value='" + obj.Title + "' />");
    $(dialog).append(descriptionEdit);

    $(dialog).dialog({
      dialogClass: 'no-title-bar',
      modal: true,
      close: function (event, ui) {
        dialog.empty();
      },
      buttons: [
        {
          text: "Ok",
          click: function () {

            var self = this;
            var title = $(descriptionEdit).val();

            $.ajax({
              url: context.editUrls[context.type] + "/" + obj.TextID,
              type: 'POST',
              headers: { 'Content-type': 'application/json' },
              dataType: 'json',
              data: JSON.stringify({ id: obj.TextID, title: title, referrerUrl: "AJAX" }),
              context: document.body,
              success: function (data) {
                $(self).dialog("close");
                $(titleElement).html(title);
              },
              error: function (e) {
                console.log(e);
              }
            });

          }
        },
        {
          text: "Cancel",
          click: function () {
            $(this).dialog("close");
          }
        }
      ]
    });

  };

  returnObj.clickHandlers["video"] = function (obj, element, e, context, titleElement) {

    var dialog = $("#editDialog");

    var descriptionLabel = $("<span class='margin-right'>Description: </span>");
    $(dialog).append(descriptionLabel);
    var descriptionEdit = $("<input type='text' value='" + obj.Description + "' />");
    $(dialog).append(descriptionEdit);

    //var br = $("<br />");
    //$(dialog).append(br);

    //var urlLabel = $("<span class='margin-right'>Url: </span>");
    //$(dialog).append(urlLabel);
    //var urlEdit = $("<input type='text' value='" + obj.Url + "' />");
    //$(dialog).append(urlEdit);

    $(dialog).dialog({
      dialogClass: 'no-title-bar',
      modal: true,
      close: function (event, ui) {
        dialog.empty();
      },
      buttons: [
        {
          text: "Ok",
          click: function () {

            var self = this;
            var description = $(descriptionEdit).val();
            //var url = $(urlEdit).val();

            $.ajax({
              url: context.editUrls[context.type] + "/" + obj.VideoID,
              type: 'POST',
              headers: { 'Content-type': 'application/json' },
              dataType: 'json',
              data: JSON.stringify({ id: obj.VideoID, description: description, referrerUrl: "AJAX" }),
              context: document.body,
              success: function (data) {
                $(self).dialog("close");
                $(titleElement).html(description);
              },
              error: function (e) {
                console.log(e);
              }
            });

          }
        },
        {
          text: "Cancel",
          click: function () {
            $(this).dialog("close");
          }
        }
      ]
    });

  };

  //////////////////////////////////END CLICK HANDLERS//////////////////////////////////


  ////////////////////////////////////OBJECT HANDLERS////////////////////////////////////

  returnObj.objectHandlers = {};

  returnObj.objectHandlers["sound"] = function (obj, locationTitle, context) {

    var div = $("<div class='sound-item'></div>");

    var h3 = $("<h3 title='View'></h3>");
    $(h3).html(obj.Description);
    $(div).append(h3);

    var h6 = $("<h6></h6>");
    var userNameA = $("<a target='_blank'></a>");
    $(userNameA).prop("href", context.userNameUrl + "/" + obj.UserName);
    $(userNameA).html(obj.UserName);
    $(h6).append(userNameA);
    $(div).append(h6);

    var br = $("<br />");
    $(div).append(br);

    if (context.currentUserName == obj.UserName) {

      var iconContainer = $('<div class="side-icon-container"></div>');

      //var aEdit = $('<a href="' + context.editUrls[context.type] + '/' + obj.SoundID + '"></a>');
      var imgEdit = $("<img class='pointer' alt='Edit' title='Edit' src='/Content/shared_images/editicon.png' />");
      if (context.clickHandlers && context.clickHandlers["sound"]) {
        $(imgEdit).click(function (e) {
          context.clickHandlers["sound"](obj, this, e, context, h3);
        });
      }
      //$(aEdit).append(imgEdit);
      $(iconContainer).append(imgEdit);

      var span = $("<span>|</span>");
      $(iconContainer).append(span);

      var formDelete = $('<form style="display: inline-block; margin: 0 !important; padding: 0 !important;" action="' + context.deleteUrls[context.type] + '" method="POST"></form>');

      var hiddenDelete = $('<input name="id" type="hidden" />');
      $(hiddenDelete).val(obj.SoundID);
      $(formDelete).append(hiddenDelete);

      var submitDelete = $('<input title="Delete" type="submit" value="" class="submit-delete-icon" />');
      $(formDelete).append(submitDelete);

      $(iconContainer).append(formDelete);

      $(div).append(iconContainer);

    }

    $(context.containerIds[context.type]).append(div);

  };

  returnObj.objectHandlers["image"] = function (obj, locationTitle, context) {

    var div = $("<div class='image-item'></div>");
    var url = context.thumbNailUrl;

    var imageDiv = $("<div class='image-item-thumb-div'></div>");
    var image = new Image();
    image.src = url + "/" + obj.ImageID;
    $(image).addClass("image-item-thumb");
    $(image).prop("title", obj.Caption);
    $(image).prop("alt", obj.Caption);
    $(image).hide();
    $(imageDiv).append(image);

    $(div).append(imageDiv);

    var br = $("<br />");
    $(div).append(br);

    var h3 = $("<h3 title='View'></h3>");
    $(h3).html(obj.Caption);
    $(div).append(h3);

    var br2 = $("<br />");
    $(div).append(br2);

    var iconContainer = $('<div class="side-icon-container"></div>');

    var aFullSize = $('<a target="_blank" href="' + context.viewUrls[context.type] + '/' + obj.ImageID + '"></a>');
    var imgFullSize = $("<img alt='Edit' title='View Full Size' src='/Content/shared_images/fullsizeicon.png' />");
    $(aFullSize).append(imgFullSize);
    $(iconContainer).append(aFullSize);

    if (context.currentUserName == obj.UserName) {

      var span = $("<span>|</span>");
      $(iconContainer).append(span);

      //var aEdit = $('<a href="' + context.editUrls[context.type] + '/' + obj.ImageID + '"></a>');
      var imgEdit = $("<img class='pointer' alt='Edit' title='Edit' src='/Content/shared_images/editicon.png' />");
      if (context.clickHandlers && context.clickHandlers["image"]) {
        $(imgEdit).click(function (e) {
          context.clickHandlers["image"](obj, this, e, context, h3);
        });
      }
      //$(aEdit).append(imgEdit);
      $(iconContainer).append(imgEdit);

      var span2 = $("<span>|</span>");
      $(iconContainer).append(span2);

      var formDelete = $('<form style="display: inline-block; margin: 0 !important; padding: 0 !important;" action="' + context.deleteUrls[context.type] + '" method="POST"></form>');

      var hiddenDelete = $('<input name="id" type="hidden" />');
      $(hiddenDelete).val(obj.SoundID);
      $(formDelete).append(hiddenDelete);

      var submitDelete = $('<input title="Delete" type="submit" value="" class="submit-delete-icon" />');
      $(formDelete).append(submitDelete);

      $(iconContainer).append(formDelete);

    }

    $(div).append(iconContainer);

    $(context.containerIds[context.type]).append(div);

    $(image).load(function () {
      $(image).show();
    });

  };

  returnObj.objectHandlers["text"] = function (obj, locationTitle, context) {

    var div = $("<div class='text-item'></div>");

    var h3 = $("<h3 title='View'></h3>");
    $(h3).html(obj.Title);
    $(div).append(h3);

    var br = $("<br />");
    $(div).append(br);

    if (context.currentUserName == obj.UserName) {

      var iconContainer = $('<div class="side-icon-container"></div>');

      //var aEdit = $('<a href="' + context.editUrls[context.type] + '/' + obj.SoundID + '"></a>');
      var imgEdit = $("<img class='pointer' alt='Edit' title='Edit' src='/Content/shared_images/editicon.png' />");
      if (context.clickHandlers && context.clickHandlers["text"]) {
        $(imgEdit).click(function (e) {
          context.clickHandlers["text"](obj, this, e, context, h3);
        });
      }
      //$(aEdit).append(imgEdit);
      $(iconContainer).append(imgEdit);

      var span = $("<span>|</span>");
      $(iconContainer).append(span);

      var formDelete = $('<form style="display: inline-block; margin: 0 !important; padding: 0 !important;" action="' + context.deleteUrls[context.type] + '" method="POST"></form>');

      var hiddenDelete = $('<input name="id" type="hidden" />');
      $(hiddenDelete).val(obj.TextID);
      $(formDelete).append(hiddenDelete);

      var submitDelete = $('<input title="Delete" type="submit" value="" class="submit-delete-icon" />');
      $(formDelete).append(submitDelete);

      $(iconContainer).append(formDelete);

      $(div).append(iconContainer);

    }

    $(context.containerIds[context.type]).append(div);

  };

  returnObj.objectHandlers["video"] = function (obj, locationTitle, context) {

    var div = $("<div class='video-item'></div>");

    var h3 = $("<h3 title='View'></h3>");
    $(h3).html(obj.Description);
    $(div).append(h3);

    var br = $("<br />");
    $(div).append(br);

    var iconContainer = $('<div class="side-icon-container"></div>');

    var aFullSize = $('<a target="_blank" href="' + context.viewUrls[context.type] + '/' + obj.VideoID + '"></a>');
    var imgFullSize = $("<img class='pointer' alt='Edit' title='View Video' src='/Content/shared_images/fullsizeicon.png' />");
    $(aFullSize).append(imgFullSize);
    $(iconContainer).append(aFullSize);

    if (context.currentUserName == obj.UserName) {

      var span = $("<span>|</span>");
      $(iconContainer).append(span);

      //var aEdit = $('<a href="' + context.editUrls[context.type] + '/' + obj.VideoID + '"></a>');
      var imgEdit = $("<img alt='Edit' title='Edit' src='/Content/shared_images/editicon.png' />");
      if (context.clickHandlers && context.clickHandlers["video"]) {
        $(imgEdit).click(function (e) {
          context.clickHandlers["video"](obj, this, e, context, h3);
        });
      }
      //$(aEdit).append(imgEdit);
      $(iconContainer).append(imgEdit);

      span = $("<span>|</span>");
      $(iconContainer).append(span);

      var formDelete = $('<form style="display: inline-block; margin: 0 !important; padding: 0 !important;" action="' + context.deleteUrls[context.type] + '" method="POST"></form>');

      var hiddenDelete = $('<input name="id" type="hidden" />');
      $(hiddenDelete).val(obj.VideoID);
      $(formDelete).append(hiddenDelete);

      var submitDelete = $('<input title="Delete" type="submit" value="" class="submit-delete-icon" />');
      $(formDelete).append(submitDelete);

      $(iconContainer).append(formDelete);

    }

    $(div).append(iconContainer);
    $(context.containerIds[context.type]).append(div);

  };

  //////////////////////////////////END OBJECT HANDLERS//////////////////////////////////


  if (argObj.hasOwnProperty("urls")) {
    returnObj.urls = argObj.urls;
  }

  if (argObj.hasOwnProperty("pageSizes")) {
    returnObj.pageSizes = argObj.pageSizes;
  }

  if (argObj.hasOwnProperty("containerIds")) {
    returnObj.containerIds = argObj.containerIds;
  }

  if (argObj.hasOwnProperty("viewUrls")) {
    returnObj.viewUrls = argObj.viewUrls;
  }

  if (argObj.hasOwnProperty("editUrls")) {
    returnObj.editUrls = argObj.editUrls;
  }

  if (argObj.hasOwnProperty("deleteUrls")) {
    returnObj.deleteUrls = argObj.deleteUrls;
  }

  if (argObj.hasOwnProperty("userNameUrl")) {
    returnObj.userNameUrl = argObj.userNameUrl;
  }

  if (argObj.hasOwnProperty("thumbNailUrl")) {
    returnObj.thumbNailUrl = argObj.thumbNailUrl;
  }

  if (argObj.hasOwnProperty("locationTitleUrl")) {
    returnObj.locationTitleUrl = argObj.locationTitleUrl;
  }

  if (argObj.hasOwnProperty("objectHandlers")) {
    returnObj.objectHandlers = argObj.objectHandlers;
  }

  if (argObj.hasOwnProperty("clickHandlers")) {
    returnObj.clickHandlers = argObj.clickHandlers;
  }

  if (argObj.hasOwnProperty("currentUserName")) {
    returnObj.currentUserName = argObj.currentUserName;
  }

  if (argObj.hasOwnProperty("currentUserId")) {
    returnObj.currentUserId = argObj.currentUserId;
  }

  if (argObj.hasOwnProperty("mode")) {
    returnObj.mode = argObj.mode;
  }

  if (argObj.hasOwnProperty("scrollContainerId")) {
    returnObj.scrollContainerId = argObj.scrollContainerId;
  }


  ////////////////////////////////////////METHODS////////////////////////////////////////

  returnObj._constructor = function () {

    var self = this;

    if (self.scrollContainerId) {

      $(self.scrollContainerId).scroll(function () {
        if ($(this).scrollTop() + $(this).height() >= ($(this).height() * self.page[self.type])) {

          self.getData();

        }
      });

    }

  };

  returnObj.getData = function () {

    try {

      var self = this;

      if (!this.pageLoaded[this.type][this.page[this.type]] &&
          this.outOfPages[this.type] == false &&
          this.scrollBusy == false) {

        this.scrollBusy = true;

        this.addLoadingSymbol();

        var postData;
        if (this.mode == "user") {
          postData = { page: this.page[this.type], pageSize: this.pageSizes[this.type] };
        }
        else {
          if (!this.currentLocationId) {
            this.scrollBusy = false;
            this.removeLoadingSymbol();
            return;
          }

          postData = { id: this.currentLocationId, page: this.page[this.type], pageSize: this.pageSizes[this.type] };
        }

        $.ajax({
          url: this.urls[this.mode][this.type],
          type: 'POST',
          headers: { 'Content-type': 'application/json' },
          dataType: 'json',
          data: JSON.stringify(postData),
          context: document.body,
          success: function (data) {

            self.removeLoadingSymbol();

            if (data.length == 0) {
              self.outOfPages[self.type] = true;
              self.scrollBusy = false;
              return;
            }

            self.pageLoaded[self.type][self.page[self.type]] = "loaded";
            self.page[self.type]++;

            self.processData(data);

            self.scrollBusy = false;
          },
          error: function (e) {
            console.log(e);
          }
        });

      }

    }
    catch (ex) {
      console.log(ex);
    }

  };

  returnObj.processData = function (objs) {

    if (objs && objs.length && objs.length > 0) {

      this.processDataRec(objs, 0);

    }

  };

  returnObj.processDataRec = function (objs, i) {

    var self = this;

    if (i < objs.length) {

      var obj = objs[i];

      if (this.locationTitles[obj.GHLocationID] == undefined) {

        $.ajax({
          url: this.locationTitleUrl,
          type: 'POST',
          headers: { 'Content-type': 'application/json' },
          dataType: 'json',
          data: JSON.stringify({ id: obj.GHLocationID }),
          context: this,
          success: function (data) {
            self.locationTitles[obj.GHLocationID] = data;
            self.objectHandlers[self.type](obj, data, self);

            self.processDataRec(objs, ++i);
          },
          error: function (e) {
            console.log(e);
          }
        });

      }
      else {
        this.objectHandlers[this.type](obj, this.locationTitles[obj.GHLocationID], self);

        this.processDataRec(objs, ++i);
      }

    }

  };

  returnObj.addLoadingSymbol = function () {

    this.loadingImg = $('<img style="margin: auto; display: block;" src="/Content/shared_images/loader.gif" />');
    $(this.containerIds[this.type]).append(this.loadingImg);

  };

  returnObj.removeLoadingSymbol = function () {

    $(this.loadingImg).remove();

  };

  returnObj.setType = function (type) {
    this.type = type;
    this.scrollBusy = false;
  };

  returnObj.setMode = function (mode) {
    this.mode = mode;
    this.scrollBusy = false;

    this.clear();

  };

  returnObj.setCurrentLocationId = function (currentLocationId) {
    this.currentLocationId = currentLocationId;
  };

  returnObj.setCurrentUserName = function (userName) {
    this.currentUserName = userName;
  };

  returnObj.clear = function () {

    this.page = {};

    this.page["sound"] = 1;
    this.page["image"] = 1;
    this.page["text"] = 1;
    this.page["video"] = 1;

    this.pageLoaded = {};

    this.pageLoaded["sound"] = {};
    this.pageLoaded["image"] = {};
    this.pageLoaded["text"] = {};
    this.pageLoaded["video"] = {};

    ////////

    this.outOfPages = {};

    this.outOfPages["sound"] = false;
    this.outOfPages["image"] = false;
    this.outOfPages["text"] = false;
    this.outOfPages["video"] = false;

  };

  //////////////////////////////////////END METHODS//////////////////////////////////////

  returnObj._constructor();
  return returnObj;

};

