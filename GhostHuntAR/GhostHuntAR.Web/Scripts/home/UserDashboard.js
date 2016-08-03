var UserDashboard = function(argObj) {

  var returnObj = {};
  
  var tempHeight = ($(window).height() - 80) - 80;
  returnObj.screenHeight = (tempHeight > 600) ? tempHeight : 600;
  returnObj.screenWidth = ($(window).width());

  returnObj.skipPage = false;
  returnObj.firstSkipTriggered = false;

  returnObj.currentScrollPage = 1;

  returnObj.setHeights = function() {
    $(".dashboard-main-content").css("height", returnObj.screenHeight);
    $(".dashboard-right-content").css("height", returnObj.screenHeight + 10);
    $(".dashboard-main-top-bottom").css("height", returnObj.screenHeight - 395);
    $(".dashboard-tab").css("height", returnObj.screenHeight - 180);
    $("#userDashboardTabs").css("height", returnObj.screenHeight - 60);
  };
  
  returnObj.buildLocationLi = function (location) {

    var self = this;

    var li = $("<li></li>");

    var listItem = $("<div class='ghlocation-list-item' ></div>");
    $(li).append(listItem);

    var idInput = $("<input type='hidden' value='" + location.GHLocationID + "' />");
    $(listItem).append(idInput);

    li.h3 = $("<h3 id='ghLocation_" + location.GHLocationID + "' class='ghlocation-list-item-activate'>" + location.Title + "</h3>");
    $(li.h3).click(function(e) {
      self.locationClick(this, e);
    });
    $(listItem).append(li.h3);

    var iconContainer = $("<div class='icon-container'></div>");
    $(listItem).append(iconContainer);
    
    if (location.CreatedByUserID == window.currentUserId) {
      var a = $("<a title='Edit' href='" + this.editLocationUrl + "/" + location.GHLocationID + "'><img alt='Edit' src='/Content/shared_images/editicon.png' /></a>");
      $(iconContainer).append(a);

      var span = $("<span class='icon-container-span'>|</span>");
      $(iconContainer).append(span);
      $(span).css("margin-left", "5px");
      $(span).css("margin-right", "5px");
    }

    var a2 = $("<a title='Add Sounds/Images/Texts/Videos' href='" + this.addSightsAndSoundsUrl + "/" + location.GHLocationID + "'><img alt='Add' src='/Content/shared_images/addicon.png' /></a>");
    $(iconContainer).append(a2);
    
    return li;

  };
  
  returnObj.parseDate = function (dateString) {
    try {
      var regex = /\d+/g;
      var ms = parseInt(dateString.match(regex));

      return new Date(ms);
    }
    catch (ex) {
      console.log(ex);
      return null;
    }
  };
  
  returnObj.setUpLocation = function (data) {

    $(".dashboard-left-header a").empty();

    var details = $(".dashboard-left-top");
    $(details).empty();

    //$(".dashboard-main-top-right").html(data.Text);
    $(".dashboard-left-header h2").html(data.Title);
    //$(".dashboard-main-top-image-caption").html(data.ImageCaption);

    var imgUrl = this.locationImageUrl + '/' + data.GHLocationID;
    var locationImg = $('<img class="dashboard-main-top-image" src="' + imgUrl + '" />');
    $(".dashboard-left-header a").append(locationImg);
    
    $(details).append("<br />");

    var addressP = $("<p></p>");
    $(addressP).html(data.AddressLine);
    $(details).append(addressP);
    
    var addressP2 = $("<p></p>");
    $(addressP2).html(data.City + ", " + data.State + " " + data.ZipPostalCode);
    $(details).append(addressP2);
    
    $(details).append("<br />");

    var latP = $("<p class='lat-long-p'>Lat: " + data.Latitude + "</p>");
    $(details).append(latP);
    
    var longP = $("<p>Long: " + data.Longitude + "</p>");
    $(details).append(longP);
    
    $(details).append("<br />");

    var soundP = $("<p></p>");
    $(soundP).html(data.SoundsCount + " sounds");
    $(details).append(soundP);

    var imageP = $("<p></p>");
    $(imageP).html(data.ImagesCount + " images");
    $(details).append(imageP);

    var textP = $("<p></p>");
    $(textP).html(data.TextsCount + " text articles");
    $(details).append(textP);
    
    $(details).append("<br />");

    var text = $("<p>" + data.Text + "</p>");
    $(".dashboard-left-top").append(text);

  };
  
  returnObj.processLocations = function(list) {

    var ul = $("#locationList");
    $(ul).empty();

    //$(".dashboard-main-top-image-container").empty();
    //$(".dashboard-main-top-details").empty();
    //$(".dashboard-main-top-right").empty();
    //$(".dashboard-main-title h1").empty();
    //$(".dashboard-main-top-image-caption").empty();

    var firstLocation = true;
    if (list) {
      for (p in list) {
        var location = list[p];
        location.DateCreated = this.parseDate(location.DateCreated);

        var li = this.buildLocationLi(location);
        $(ul).append(li);

        if (firstLocation == true) {
          $(li.h3).trigger("click");

          firstLocation = false;
        }
      }
    }
    //no items returned
    if (firstLocation == true) {
      this.sasa.clear();
      this.sasa.setCurrentLocationId(null);
      $(".dashboard-tab").empty();
      $(".dashboard-left-top").empty();
      $(".dashboard-left-header a").empty();
      $(".dashboard-left-header h2").html("No Results Returned");
    }
  };

  returnObj.locationClick = function (context, event, skipFirstPage) {

    var self = this;
    
    if (this.firstSkipTriggered == false) {

      this.skipPage = false;

      this.firstSkipTriggered = true;
    }
    else {
      this.skipPage = true;
    }

    this.currentScrollPage = 1;
    //console.log("Current Scroll Page locationClick " + this.currentScrollPage);

    $(".ghlocation-list-item").removeClass("ghlocation-list-item-active");

    var parent = $(context).parent();
    $(parent).addClass("ghlocation-list-item-active");
    var idInp = ($(parent).children())[0];
    var id = $(idInp).val();

    $(".dashboard-tab").empty();

    this.sasa.locationTitles = window.locationTitles;
    this.sasa.setCurrentLocationId(id);
    this.sasa.clear();
    this.sasa.getData();

    if (!window.locationObjs[id]) {

      $.ajax({
        url: this.indexLocationUrl,
        type: 'POST',
        headers: { 'Content-type': 'application/json' },
        dataType: 'json',
        data: JSON.stringify({ id: id }),
        context: document.body,
        success: function (data) {

          window.locationObjs[parseInt(data.GHLocationID)] = data;
          self.setUpLocation(data);

        },
        error: function (e) {
          console.log(e);
        }
      });

    }
    else {
      this.setUpLocation(window.locationObjs[id]);
    }

  };

  returnObj.searchSubmit = function(context, event) {

    var self = this;

    var container = $("#controlsAccordion");
    var width = $(container).width();
    var height = $(container).height();
    var offset = $(container).offset();

    var modalOverlay = $("<div class='modal'></div>");
    var loadingImageDiv = $("<div style='margin: auto; text-align: center;'><img src='../../Content/shared_images/loader.gif'/></div>");
    $(loadingImageDiv).css("margin-top", (height / 2) - 8);
    $(modalOverlay).append(loadingImageDiv);
    $(modalOverlay).width(width);
    $(modalOverlay).height(height);
    $(modalOverlay).css("top", offset.top + "px");
    $(modalOverlay).css("left", offset.left + "px");
    $('body').append(modalOverlay);

    this.skipPage = true;

    var keywords = $("#keywords").val();
    var address = $("#address").val();
    var distanceType = $("#distanceType").val();
    var radius = $("#radius").val();

    var postData = {
      keywords: keywords,
      address: address,
      distanceType: distanceType,
      radius: radius
    };

    $.ajax({
      url: this.indexSearchUrl,
      type: 'POST',
      headers: { 'Content-type': 'application/json' },
      dataType: 'json',
      data: JSON.stringify(postData),
      context: document.body,
      success: function (data) {
        self.processLocations(data);       
        $(".home-userdashboard-controls-container input[type='text']").each(function () {
          $(this).val(null);
        });
        $(".home-userdashboard-controls-container select").each(function() {
          $(this).val($(this).find("option:first").val());
        });
        $(modalOverlay).remove();
      },
      error: function (e) {
        $(modalOverlay).remove();
        console.log(e);
      }
    });
    
  };
  
  ////////////////////////////////////////////////////////////////

  returnObj.editLocationUrl = argObj.editLocationUrl;
  returnObj.addSightsAndSoundsUrl = argObj.addSightsAndSoundsUrl;
  returnObj.locationImageUrl = argObj.locationImageUrl;
  returnObj.indexLocationUrl = argObj.indexLocationUrl;
  returnObj.indexSearchUrl = argObj.indexSearchUrl;
  returnObj.indexHomeUrl = argObj.indexHomeUrl;
  returnObj.sasa = argObj.sasa;
  
  ////////////////////////////////////////////////////////////////

  return returnObj;

};
  
