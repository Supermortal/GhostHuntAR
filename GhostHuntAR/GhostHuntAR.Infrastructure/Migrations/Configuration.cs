using System;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Security;
using GhostHuntAR.Infrastructure.Models;
using WebMatrix.WebData;

namespace GhostHuntAR.Infrastructure.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<GhostHuntAR.Infrastructure.Models.EFContext>
  {

    private EFContext db;

    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(GhostHuntAR.Infrastructure.Models.EFContext context)
    {
      db = context;

      if (!WebSecurity.Initialized)
        WebSecurity.InitializeDatabaseConnection("EFContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

      SeedRoles();
      SeedAdminUser();
      SeedGHLocations();
      SeedSounds();
      SeedImages();
      CreateIndexes();
      CreateConstraints();
    }

    private void SeedSounds()
    {
      FileStream fs = null;
      try
      {
        fs = File.OpenRead(@"C:\Users\user\Dropbox\Cloud\Projects\Ghosts\SeedData\test.mp3");
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, Convert.ToInt32(fs.Length));

        if (fs != null)
        {
          fs.Close();
          fs.Dispose();
        }


        for (var i = 0; i < 100; i++)
        {
          var rawSound = new RawSound();
          rawSound.Data = bytes;
          rawSound.FileName = "test.mp3";
          rawSound.MIMEType = "audio/mpeg";

          var s = new Sound();
          s.GHLocationID = 1;
          s.Description = "Test Description " + i;
          s.UserName = "admin";
          db.Sounds.Add(s);

          db.SaveChanges();

          rawSound.RawSoundID = s.SoundID;
          db.RawSounds.Add(rawSound);

          db.SaveChanges();

          rawSound = new RawSound();
          rawSound.Data = bytes;
          rawSound.FileName = "test.mp3";
          rawSound.MIMEType = "audio/mpeg";

          s = new Sound();
          s.GHLocationID = 2;
          s.Description = "Test Description " + i;
          s.UserName = "admin";
          db.Sounds.Add(s);

          db.SaveChanges();

          rawSound.RawSoundID = s.SoundID;
          db.RawSounds.Add(rawSound);

          db.SaveChanges();
        }
      }
      finally
      {
        if (fs != null)
        {
          fs.Close();
          fs.Dispose();
        }
      }
    }

    private void SeedImages()
    {
      FileStream fs = null;
      try
      {
        fs = File.OpenRead(@"C:\Users\user\Dropbox\Cloud\Projects\Ghosts\SeedData\test.jpg");

        for (var j = 0; j < 100; j++)
        {
          var temp = System.Drawing.Image.FromStream(fs);
          var ms = new MemoryStream();
          temp.Save(ms, ImageFormat.Png);
          var data = ms.ToArray();

          var i = new Models.Image();
          i.Caption = "Test Caption " + j;
          i.UserName = "admin";
          i.GHLocationID = 1;

          ms.Dispose();
          ms = new MemoryStream();
          var myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
          var thumbNail = temp.GetThumbnailImage(50, 50, myCallback, IntPtr.Zero);
          thumbNail.Save(ms, ImageFormat.Png);
          i.Thumbnail = ms.ToArray();

          db.Images.Add(i);
          db.SaveChanges();

          var ri = new RawImage();
          ri.FileName = "test.jpg";
          ri.MIMEType = "image/png";
          ri.Data = data;
          ri.RawImageID = i.ImageID;

          db.RawImages.Add(ri);
        }
      }
      finally
      {
        if (fs != null)
        {
          fs.Close();
          fs.Dispose();
        }
      }
    }

    public bool ThumbnailCallback()
    {
      return false;
    }

    private void SeedRoles()
    {
      if (!Roles.RoleExists("admin"))
        Roles.CreateRole("admin");

      if (!Roles.RoleExists("user"))
        Roles.CreateRole("user");

      if (!Roles.RoleExists("pro_user"))
        Roles.CreateRole("pro_user");
    }

    private void SeedAdminUser()
    {
      FileStream fs = null;
      try
      {
        fs = File.OpenRead(@"C:\Users\user\Dropbox\Cloud\Projects\Ghosts\SeedData\test.jpg");

        var temp = System.Drawing.Image.FromStream(fs);
        var ms = new MemoryStream();
        temp.Save(ms, ImageFormat.Png);
        var data = ms.ToArray();

        string Biography =
          "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In fringilla tincidunt elit ut elementum. Vivamus ut velit vel ante aliquet molestie. Fusce facilisis turpis vitae eros pharetra viverra. Integer ultricies est vel feugiat tincidunt. Praesent dictum facilisis eros, id consectetur risus tincidunt ultricies. Nam ullamcorper nunc magna, vitae bibendum ipsum dictum eu. Nam semper aliquet fringilla. Aliquam mauris velit, posuere vitae neque non, gravida tempus enim. Sed euismod eros et dolor laoreet eleifend. Duis vel adipiscing libero. Vivamus ultrices at sapien nec tempor. Sed enim velit, cursus in dolor eget, convallis fringilla lectus. </p> " +

          "<p>Suspendisse nisl orci, bibendum in viverra nec, porttitor sed libero. Duis venenatis vestibulum pharetra. Sed vestibulum auctor lorem, eu sodales nibh vehicula at. Phasellus fringilla leo vel nisl imperdiet posuere. Duis dignissim blandit enim et aliquam. Sed vitae massa et enim suscipit varius vel a orci. Aenean enim quam, tincidunt non nunc in, interdum hendrerit enim. Mauris eget libero lacinia, tincidunt arcu vel, vulputate lectus. Proin vel diam at dolor suscipit rhoncus eu nec dui. Nulla erat orci, pharetra in porttitor nec, ultrices quis eros. Sed faucibus luctus cursus. Phasellus eget congue enim, at lacinia diam. Nulla quis dictum libero. Sed pulvinar sodales nisl, a cursus arcu malesuada fermentum. Aliquam erat volutpat. </p>  " +

          "<p>Vivamus mollis nisl id dapibus vulputate. In vitae iaculis risus, in dignissim lacus. Donec id condimentum nibh. Integer laoreet fermentum orci. Aliquam vulputate, enim eu pretium congue, sapien risus congue ante, facilisis sodales felis turpis non velit. Suspendisse at neque ut enim tristique euismod at eget arcu. Mauris tincidunt commodo purus, ut volutpat ligula volutpat ut. Curabitur ornare sem sit amet arcu cursus convallis. Cras vitae velit vel libero volutpat condimentum. Ut volutpat ac lacus id rutrum. In quis pellentesque metus. Duis risus justo, auctor a posuere ut, ornare sit amet odio. Sed mattis semper mollis. </p>  " +

          "<p>Donec malesuada posuere urna quis rutrum. Morbi commodo mi turpis, ut ullamcorper felis sodales id. Proin eget velit sagittis, placerat tortor eu, mollis massa. Integer pulvinar quis dolor non gravida. Nulla molestie vel metus nec pulvinar. Donec eget purus elit. Nullam pulvinar justo nec sapien venenatis malesuada. Praesent commodo consequat lectus nec sagittis. Curabitur laoreet arcu nec est egestas dapibus. Quisque turpis risus, blandit vel bibendum non, faucibus aliquam urna. Quisque fringilla ante ante, sit amet congue massa ornare at. Phasellus molestie odio et risus euismod dapibus. Nam in nulla nisi. </p>  " +

          "<p>Donec pellentesque, neque nec condimentum porttitor, turpis lacus vulputate justo, nec dapibus eros sapien eget ligula. Nunc bibendum turpis eu enim varius, id sodales tellus scelerisque. Suspendisse potenti. Etiam in leo sodales, feugiat nisl consequat, sodales purus. In aliquam vestibulum risus nec tempus. Pellentesque quis tellus eget tortor convallis dictum. Praesent ut malesuada eros. Aenean vitae eros et mauris blandit venenatis. Integer rhoncus aliquam diam. Donec vulputate sagittis erat, et congue erat. Proin sit amet laoreet nibh. Nulla tempor leo ut justo auctor, porta mollis nisl egestas. Sed faucibus turpis ac mi tincidunt, eu dignissim felis congue. </p>";

        WebSecurity.CreateUserAndAccount("admin", "9d7e8737564e64994d57aff1820f9c0e23041ebc54f983b949b7be5a348c18f6",
          new { Role = "admin", Email = "admin@namespacemedia.com", Name = "Administrator", Biography, Image = data });

        if (!Roles.IsUserInRole("admin"))
          Roles.AddUserToRole("admin", "admin");

        var user = db.UserProfiles.Single(u => u.UserName == "admin");
        user.LastUserSettings = new LastUserSettings();
        user.LastUserSettings.UserId = user.UserId;
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();

        WebSecurity.CreateUserAndAccount("testuser",
          "9d7e8737564e64994d57aff1820f9c0e23041ebc54f983b949b7be5a348c18f6",
          new { Role = "user", Email = "test@test.com", Name = "Test User", Biography, Image = data });

        if (!Roles.IsUserInRole("user"))
          Roles.AddUserToRole("testuser", "user");

        user = db.UserProfiles.Single(u => u.UserName == "testuser");
        user.LastUserSettings = new LastUserSettings();
        user.LastUserSettings.UserId = user.UserId;
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();

        WebSecurity.CreateUserAndAccount("testprouser",
          "9d7e8737564e64994d57aff1820f9c0e23041ebc54f983b949b7be5a348c18f6",
          new { Role = "pro_user", Email = "test@test.com", Name = "Test Pro User", Biography, Image = data });

        if (!Roles.IsUserInRole("pro_user"))
          Roles.AddUserToRole("testprouser", "pro_user");

        user = db.UserProfiles.Single(u => u.UserName == "testprouser");
        user.LastUserSettings = new LastUserSettings();
        user.LastUserSettings.UserId = user.UserId;
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();

      }
      finally
      {
        if (fs != null)
        {
          fs.Close();
          fs.Dispose();
        }
      }
    }

    private void SeedGHLocations()
    {
      FileStream fs = null;

      try
      {
        fs = File.OpenRead(@"C:\Users\user\Dropbox\Cloud\Projects\Ghosts\SeedData\test_location.jpg");

        var temp = System.Drawing.Image.FromStream(fs);
        var ms = new MemoryStream();
        temp.Save(ms, ImageFormat.Png);
        var data = ms.ToArray();

        var user = db.UserProfiles.Find(1);

        var location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption";
        location.Title = "My House";
        location.Altitude = 1276.7;
        location.Latitude = 40.769119;
        location.Longitude = -111.859180;
        location.CreatedByUserID = 1;
        location.Text =
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque aliquet, mi id tempor pulvinar, lacus arcu bibendum ipsum, in dictum massa mauris nec urna. Ut vestibulum iaculis leo ut fringilla. Quisque varius bibendum felis id commodo. Proin orci ipsum, porttitor nec tortor et, faucibus facilisis diam. Nulla lacinia, nibh in cursus interdum, lacus mauris dignissim risus, elementum luctus ante erat commodo leo. Nullam volutpat turpis ligula, quis pellentesque enim tincidunt vitae. Vestibulum et mi vitae justo ornare lobortis." +
          "Maecenas et nunc sit amet quam porttitor feugiat. Duis ultricies, justo vel pharetra placerat, velit dui porttitor magna, non imperdiet augue libero eu neque. Etiam tempor nisi vitae lacus pulvinar facilisis. Proin ut posuere nibh. Suspendisse ac est posuere, convallis justo eu, venenatis odio. Integer scelerisque justo id erat feugiat interdum. Praesent auctor fermentum odio ut pretium." +
          "Maecenas malesuada vulputate mi, vitae pharetra arcu rhoncus ut. Quisque accumsan dapibus ante in porta. Nullam vel rhoncus neque. Proin tincidunt pellentesque felis vitae posuere. Phasellus elementum pharetra dolor nec scelerisque. Curabitur id lorem hendrerit, sodales nisl volutpat, suscipit augue. Aliquam laoreet tellus a tristique feugiat. Morbi pharetra mi et diam euismod congue. Fusce sed consequat justo. Ut elit turpis, auctor varius diam sit amet, pharetra molestie dui. Sed quis suscipit felis.";
        user.GHLocations.Add(location);

        location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption";
        location.Title = "Wal Mart";
        location.Altitude = 1276.7;
        location.Latitude = 40.625304;
        location.Longitude = -111.99504688;
        location.CreatedByUserID = 1;
        location.Text =
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque aliquet, mi id tempor pulvinar, lacus arcu bibendum ipsum, in dictum massa mauris nec urna. Ut vestibulum iaculis leo ut fringilla. Quisque varius bibendum felis id commodo. Proin orci ipsum, porttitor nec tortor et, faucibus facilisis diam. Nulla lacinia, nibh in cursus interdum, lacus mauris dignissim risus, elementum luctus ante erat commodo leo. Nullam volutpat turpis ligula, quis pellentesque enim tincidunt vitae. Vestibulum et mi vitae justo ornare lobortis." +
          "Maecenas et nunc sit amet quam porttitor feugiat. Duis ultricies, justo vel pharetra placerat, velit dui porttitor magna, non imperdiet augue libero eu neque. Etiam tempor nisi vitae lacus pulvinar facilisis. Proin ut posuere nibh. Suspendisse ac est posuere, convallis justo eu, venenatis odio. Integer scelerisque justo id erat feugiat interdum. Praesent auctor fermentum odio ut pretium." +
          "Maecenas malesuada vulputate mi, vitae pharetra arcu rhoncus ut. Quisque accumsan dapibus ante in porta. Nullam vel rhoncus neque. Proin tincidunt pellentesque felis vitae posuere. Phasellus elementum pharetra dolor nec scelerisque. Curabitur id lorem hendrerit, sodales nisl volutpat, suscipit augue. Aliquam laoreet tellus a tristique feugiat. Morbi pharetra mi et diam euismod congue. Fusce sed consequat justo. Ut elit turpis, auctor varius diam sit amet, pharetra molestie dui. Sed quis suscipit felis.";
        user.GHLocations.Add(location);

        location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption";
        location.Title = "Target";
        location.Altitude = 1276.7;
        location.Latitude = 40.623457;
        location.Longitude = -111.851171;
        location.CreatedByUserID = 1;
        location.Text =
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque aliquet, mi id tempor pulvinar, lacus arcu bibendum ipsum, in dictum massa mauris nec urna. Ut vestibulum iaculis leo ut fringilla. Quisque varius bibendum felis id commodo. Proin orci ipsum, porttitor nec tortor et, faucibus facilisis diam. Nulla lacinia, nibh in cursus interdum, lacus mauris dignissim risus, elementum luctus ante erat commodo leo. Nullam volutpat turpis ligula, quis pellentesque enim tincidunt vitae. Vestibulum et mi vitae justo ornare lobortis." +
          "Maecenas et nunc sit amet quam porttitor feugiat. Duis ultricies, justo vel pharetra placerat, velit dui porttitor magna, non imperdiet augue libero eu neque. Etiam tempor nisi vitae lacus pulvinar facilisis. Proin ut posuere nibh. Suspendisse ac est posuere, convallis justo eu, venenatis odio. Integer scelerisque justo id erat feugiat interdum. Praesent auctor fermentum odio ut pretium." +
          "Maecenas malesuada vulputate mi, vitae pharetra arcu rhoncus ut. Quisque accumsan dapibus ante in porta. Nullam vel rhoncus neque. Proin tincidunt pellentesque felis vitae posuere. Phasellus elementum pharetra dolor nec scelerisque. Curabitur id lorem hendrerit, sodales nisl volutpat, suscipit augue. Aliquam laoreet tellus a tristique feugiat. Morbi pharetra mi et diam euismod congue. Fusce sed consequat justo. Ut elit turpis, auctor varius diam sit amet, pharetra molestie dui. Sed quis suscipit felis.";
        user.GHLocations.Add(location);

        location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption";
        location.Title = "Taco Bell";
        location.Altitude = 1276.7;
        location.Latitude = 40.623457;
        location.Longitude = -111.835058;
        location.CreatedByUserID = 1;
        location.Text =
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque aliquet, mi id tempor pulvinar, lacus arcu bibendum ipsum, in dictum massa mauris nec urna. Ut vestibulum iaculis leo ut fringilla. Quisque varius bibendum felis id commodo. Proin orci ipsum, porttitor nec tortor et, faucibus facilisis diam. Nulla lacinia, nibh in cursus interdum, lacus mauris dignissim risus, elementum luctus ante erat commodo leo. Nullam volutpat turpis ligula, quis pellentesque enim tincidunt vitae. Vestibulum et mi vitae justo ornare lobortis." +
          "Maecenas et nunc sit amet quam porttitor feugiat. Duis ultricies, justo vel pharetra placerat, velit dui porttitor magna, non imperdiet augue libero eu neque. Etiam tempor nisi vitae lacus pulvinar facilisis. Proin ut posuere nibh. Suspendisse ac est posuere, convallis justo eu, venenatis odio. Integer scelerisque justo id erat feugiat interdum. Praesent auctor fermentum odio ut pretium." +
          "Maecenas malesuada vulputate mi, vitae pharetra arcu rhoncus ut. Quisque accumsan dapibus ante in porta. Nullam vel rhoncus neque. Proin tincidunt pellentesque felis vitae posuere. Phasellus elementum pharetra dolor nec scelerisque. Curabitur id lorem hendrerit, sodales nisl volutpat, suscipit augue. Aliquam laoreet tellus a tristique feugiat. Morbi pharetra mi et diam euismod congue. Fusce sed consequat justo. Ut elit turpis, auctor varius diam sit amet, pharetra molestie dui. Sed quis suscipit felis.";
        user.GHLocations.Add(location);

        user = db.UserProfiles.Find(3);

        location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption 2";
        location.Title = "Empire State Building";
        location.Latitude = 40.67206;
        location.Longitude = -73.983898;
        location.CreatedByUserID = 3;
        user.GHLocations.Add(location);

        user = db.UserProfiles.Find(2);

        location = new GHLocation();
        location.Image = data;
        location.ImageCaption = "Test Caption 3";
        location.Title = "Sinclair Gas Station";
        location.Latitude = 41.180481;
        location.Longitude = -111.995202;
        location.CreatedByUserID = 2;
        location.Text =
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque aliquet, mi id tempor pulvinar, lacus arcu bibendum ipsum, in dictum massa mauris nec urna. Ut vestibulum iaculis leo ut fringilla. Quisque varius bibendum felis id commodo. Proin orci ipsum, porttitor nec tortor et, faucibus facilisis diam. Nulla lacinia, nibh in cursus interdum, lacus mauris dignissim risus, elementum luctus ante erat commodo leo. Nullam volutpat turpis ligula, quis pellentesque enim tincidunt vitae. Vestibulum et mi vitae justo ornare lobortis." +
          "Maecenas et nunc sit amet quam porttitor feugiat. Duis ultricies, justo vel pharetra placerat, velit dui porttitor magna, non imperdiet augue libero eu neque. Etiam tempor nisi vitae lacus pulvinar facilisis. Proin ut posuere nibh. Suspendisse ac est posuere, convallis justo eu, venenatis odio. Integer scelerisque justo id erat feugiat interdum. Praesent auctor fermentum odio ut pretium." +
          "Maecenas malesuada vulputate mi, vitae pharetra arcu rhoncus ut. Quisque accumsan dapibus ante in porta. Nullam vel rhoncus neque. Proin tincidunt pellentesque felis vitae posuere. Phasellus elementum pharetra dolor nec scelerisque. Curabitur id lorem hendrerit, sodales nisl volutpat, suscipit augue. Aliquam laoreet tellus a tristique feugiat. Morbi pharetra mi et diam euismod congue. Fusce sed consequat justo. Ut elit turpis, auctor varius diam sit amet, pharetra molestie dui. Sed quis suscipit felis.";
        user.GHLocations.Add(location);
      }
      finally
      {
        if (fs != null)
        {
          fs.Close();
          fs.Dispose();
        }
      }

    }

    private void CreateIndexes()
    {
      var sql = string.Empty;
      var sb = new StringBuilder();

      //UserProfile
      sb.Append("CREATE INDEX userName_UserProfile ON UserProfile (UserName); ");

      //Sessions
      sb.Append("CREATE INDEX userName_Sessions ON Sessions (UserName); ");

      //GHLocations
      sb.Append("CREATE INDEX title_GHLocations ON GHLocations (Title); ");
      sb.Append("CREATE INDEX City_GHLocations ON GHLocations (City); ");
      sb.Append("CREATE INDEX State_GHLocations ON GHLocations (State); ");
      sb.Append("CREATE INDEX AddressLine_GHLocations ON GHLocations (AddressLine); ");
      sb.Append("CREATE INDEX CreatedByUserID_GHLocations ON GHLocations (CreatedByUserID); ");

      //Images
      sb.Append("CREATE INDEX Caption_Images ON Images (Caption); ");
      sb.Append("CREATE INDEX UserName_Images ON Images (UserName); ");

      //Sounds
      sb.Append("CREATE INDEX Description_Sounds ON Sounds (Description); ");
      sb.Append("CREATE INDEX UserName_Sounds ON Sounds (UserName); ");

      //Texts
      sb.Append("CREATE INDEX Title_Texts ON Texts (Title); ");
      sb.Append("CREATE INDEX UserName_Texts ON Texts (UserName); ");

      //PotentialUsers
      sb.Append("CREATE INDEX Token_PotentialUsers ON PotentialUsers (Token); ");

      sql = sb.ToString();
      db.Database.ExecuteSqlCommand(sql);
    }

    private void CreateConstraints()
    {
      var sql = string.Empty;
      var sb = new StringBuilder();

      sb.Append("ALTER TABLE GHLocations ");
      sb.Append("ADD CONSTRAINT AK_GHLocations_Latitude_Longitude UNIQUE (Latitude, Longitude); ");

      sql = sb.ToString();
      db.Database.ExecuteSqlCommand(sql);
    }

  }
}
