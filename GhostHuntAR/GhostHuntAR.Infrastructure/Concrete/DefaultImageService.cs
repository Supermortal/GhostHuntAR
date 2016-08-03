using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using GhostHuntAR.Infrastructure.Abstract;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class DefaultImageService : IImageService 
  {

    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (typeof(DefaultImageService));

    public void RestoreOriginalImage(string imagePath)
    {
      try
      {

          imagePath = imagePath.Replace(@"\\", "/").Replace("\\", "/").Replace("PostImages", "_PostImages");

        var originalImagePath = GetOriginalImagePath(imagePath);

        if (File.Exists(originalImagePath))
        {

          if (File.Exists(imagePath))
            File.Delete(imagePath);

          var image = Image.FromFile(originalImagePath);
          var memoryStream = new MemoryStream();

          image.Save(memoryStream, image.RawFormat);

          image.Dispose();
          File.Delete(originalImagePath);

          image = Image.FromStream(memoryStream);
          image.Save(imagePath);

        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

    public void ResizeImage(string imagePath, int width, int height)
    {
      try
      {

          imagePath = imagePath.Replace(@"\\", "/").Replace("\\", "/").Replace("PostImages", "_PostImages");

        var image = GetImage(imagePath);

        var memoryStream = new MemoryStream();
        image.Save(memoryStream, image.RawFormat);

        image.Dispose();
        File.Delete(imagePath);

        image = Image.FromStream(memoryStream);

        SaveOriginalImage(imagePath, ref image);

        var resizedImage = new Bitmap(image, new Size(width, height));
        resizedImage.Save(imagePath);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

      public void SaveImage(HttpPostedFileBase hpfb, string fullPath)
      {
          try
          {

              if (hpfb == null || hpfb.ContentLength == 0) return;

              var temp = hpfb.ContentType;
              var parts = temp.Split('/');
              var type = parts[0];

              if (type.ToLower() != "image")
                  return;

              hpfb.SaveAs(fullPath + "/" + hpfb.FileName);

          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
          }
      }

      //IQueryable<Models.Image> IImageService.GetAllImages()
      //{
      //    throw new NotImplementedException();
      //}

      public Models.Image FindImage(string fileName, HttpServerUtilityBase server)
      {
          try
          {
              var i = System.Drawing.Image.FromFile(server.MapPath("/_PostImages/") + fileName);

              var image = new Models.Image();
              image.FileName = fileName;
              image.Width = i.Width;
              image.Height = i.Height;

              var ms = new MemoryStream();
              i.Save(ms, i.RawFormat);
              image.Data = ms.ToArray();

              i.Dispose();

              return image;
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return null;
          }
      }

      public IQueryable<PostImageViewModel> GetAllPostImageViewModels(HttpServerUtilityBase server, HttpRequestBase request, int page, int pageSize)
      {
          try
          {

              var di = new DirectoryInfo(server.MapPath("/_PostImages"));
              var list = new List<PostImageViewModel>();

              var index = 0;
              var min = (page - 1) * pageSize;
              var max = min + pageSize;
              foreach (var file in di.GetFiles())
              {

                  if (index < min)
                  {
                      index++;
                      continue;
                  }

                  var pivm = new PostImageViewModel();

                  pivm.FileName = file.Name;

                  var url = file.FullName.Replace(request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty).Replace('\\', '/');
                  url = url.TrimStart('_');
                  pivm.Url = "/" + url;

                  var image = System.Drawing.Image.FromFile(file.FullName);

                  pivm.Width = image.Width;
                  pivm.Height = image.Height;

                  image.Dispose();

                  list.Add(pivm);

                  index++;

                  if (index >= max)
                      break;

              }

              return list.AsQueryable();

          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return (new List<PostImageViewModel>()).AsQueryable();
          }
      }

      public int GetImageCount(HttpServerUtilityBase server)
      {
          try
          {
              return (new DirectoryInfo(server.MapPath("/_PostImages"))).GetFiles().Count();
          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
              return -1;
          }
      }

      public void DeleteImage(string fileName, HttpServerUtilityBase server)
      {
          try
          {

              if (File.Exists(server.MapPath("/_PostImages/" + fileName)))
                  System.IO.File.Delete(server.MapPath("/_PostImages/" + fileName));

              var parts = fileName.Split('.');

              if (File.Exists(server.MapPath("/_PostImages/OriginalImages/" + parts[0] + "_original." + parts[1])))
                  File.Delete(server.MapPath("/_PostImages/OriginalImages/" + parts[0] + "_original." + parts[1]));

          }
          catch (Exception ex)
          {
              Log.Error(ex.Message, ex);
          }
      }

      public IQueryable<Image> GetAllImages()
      {
          throw new NotImplementedException();
      }

      public Image Find(long id)
      {
          throw new NotImplementedException();
      }

      public Image FindResizedImage(string fileName)
      {
          throw new NotImplementedException();
      }

      public Image FindOriginalImage(string fileName)
      {
          throw new NotImplementedException();
      }

      private string GetOriginalImagePath(string imagePath)
      {
        try
        {

        var imagePathParts = imagePath.Split('/');
        var fileName = imagePathParts[imagePathParts.Length - 1];

        var fileNameParts = fileName.Split('.');
        var originalFileName = fileNameParts[0] + "_original." + fileNameParts[1];

        var originalImagePath = string.Empty;
        for (var i = 0; i < imagePathParts.Length - 1; i++)
        {
            originalImagePath += imagePathParts[i] + "/";
        }
        originalImagePath += "OriginalImages/" + originalFileName;

        return originalImagePath;

        }
        catch (Exception ex)
        {
        Log.Error(ex.Message, ex);
        return null;
        }
    }

    private Image GetImage(string imagePath)
    {
      try
      {

        var originalImagePath = GetOriginalImagePath(imagePath);

        if (File.Exists(originalImagePath))
          return Image.FromFile(originalImagePath);
        else
          return Image.FromFile(imagePath);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return null;
      }
    }

    private void SaveOriginalImage(string imagePath, ref Image image)
    {
      try
      {

        var originalImagePath = GetOriginalImagePath(imagePath);

        var parts = originalImagePath.Split('/');
        var originalImageDirectory = string.Empty;
        for (var i = 0; i < parts.Length - 1; i++)
        {
          originalImageDirectory += parts[i] + "/";
        }
        originalImageDirectory = originalImageDirectory.TrimEnd('/');

        if (!Directory.Exists(originalImageDirectory))
          Directory.CreateDirectory(originalImageDirectory);

        if (!File.Exists(originalImagePath))
          image.Save(originalImagePath);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
      }
    }

  }
}