using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostHuntAR.Infrastructure.Abstract
{
  public interface IEmailTemplateService : IDisposable
  {
    void Configure(string templateDirectory);
    Dictionary<string, object> GetTemplateBindings(string templateName);
    string SetTemplateBindings(Dictionary<string, object> bindings);
  }
}
