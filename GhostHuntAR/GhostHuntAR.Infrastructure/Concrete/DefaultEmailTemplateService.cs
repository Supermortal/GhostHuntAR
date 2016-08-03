using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GhostHuntAR.Infrastructure.Abstract;
using log4net;
using NamespaceMedia.Common.Abstract.Email;
using NamespaceMedia.Common.Concrete.Email;
using NamespaceMedia.Common.Helpers.Log;

namespace GhostHuntAR.Infrastructure.Concrete
{
  public class DefaultEmailTemplateService : IEmailTemplateService
  {

    private static readonly ILog Log = LogHelper.GetLogger
    (typeof(DefaultEmailTemplateService));

    private readonly IEmailTemplateBindingService _etbs;
    private Dictionary<string, object> _bindings;
    private string _template;

    private DirectoryInfo DirectoryInfo { get; set; }
    
    public DefaultEmailTemplateService(IEmailTemplateBindingService etbs)
    {
      _etbs = etbs;
    }

    public void Dispose()
    {
      try
      {
        _bindings = null;
        _template = null;
        DirectoryInfo = null;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public void Configure(string templateDirectory)
    {
      try
      {
        templateDirectory = templateDirectory.Replace('/', '\\');
        var regex = new Regex("\\.\\.\\\\");
        var matches = regex.Matches(templateDirectory);
        var count = matches.Count;

        templateDirectory = Regex.Replace(templateDirectory, "\\.\\.\\\\", string.Empty);

        var appDomain = AppDomain.CurrentDomain.BaseDirectory;
        var parts = appDomain.Split('\\');
        var sb = new StringBuilder();
        for (var i = 0; i < (parts.Length - count - 1); i++)
        {
          sb.Append(parts[i]);
          sb.Append("\\");
        }

        if (templateDirectory.StartsWith("\\"))
          templateDirectory = templateDirectory.TrimStart('\\');

        var directory = sb + templateDirectory;

        DirectoryInfo = new DirectoryInfo(directory);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public Dictionary<string, object> GetTemplateBindings(string templateName)
    {
      try
      {
        if (DirectoryInfo == null || !DirectoryInfo.Exists)
          throw new EmailTemplateServiceNotConfiguredException();

        if (!templateName.Contains("."))
          templateName += ".*";

        var files = DirectoryInfo.GetFiles(templateName);
        var templateFile = files.FirstOrDefault();
        if (templateFile == null)
          return null;

        var sb = new StringBuilder();
        using (var sr = templateFile.OpenText())
        {
            var s = "";
            while ((s = sr.ReadLine()) != null)
            {
              sb.Append(s);
            }
        }

        _template = sb.ToString();
        _bindings = new Dictionary<string, object>();

        _etbs.GetBindingVariables(ref _template, ref _bindings);

        return _bindings;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

    public string SetTemplateBindings(Dictionary<string, object> bindings)
    {
      try
      {
        _etbs.SetBindingVariables(ref _template, ref bindings);
        return _template;
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        throw ex;
      }
    }

  }
}