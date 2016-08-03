
var Home = function (ud, sasa) {

  $("#locationsTypeSubmitButton").click(function() {
    var value = $("#locationType").val();

    location.href = value;
  });

  $("#searchSubmitButton").click(function (e) {
    ud.searchSubmit(this, e);
  });

  $("#controlsAccordion").accordion({ collapsible: true, active: false, icons: false, heightStyle: 'content' });
  $("#controlsAccordion").show();

  //ud.setHeights();
  $(".dashboard-main-top-bottom").show();

  $("#userDashboardTabs").tabs({
    beforeActivate: function (event, ui) {
      var newTab = ui.newTab;
      var a = ($(newTab).children())[0];
      var type = $(a).html().toLowerCase();

      ud.currentScrollPage = 1;

      sasa.setType(type);
      sasa.getData();
    }
  });
  $("#userDashboardTabs").show();

  sasa.setType("sound");
  sasa.setMode("location");

  $(".ghlocation-list-item-activate").click(function(e) {
    ud.locationClick(this, e, false);
  });

  var first = $(".ghlocation-list-item-activate")[0];
  $(first).trigger("click");
  
  ud.currentScrollPage = 1;
  $(".dashboard-tab").scroll(function () {
    if ($(this).scrollTop() + $(this).height() >= ($(this).height() * ud.currentScrollPage)) {

      if (ud.skipPage == false) {
        sasa.getData();
        console.log("Current Scroll Page scroll " + ud.currentScrollPage);
        ud.currentScrollPage++;
      }
      
      if (ud.skipPage == true) {
        ud.skipPage = false;
      }

    }
  });

};



