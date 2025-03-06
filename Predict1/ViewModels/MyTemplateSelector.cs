using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;

namespace Predict1.ViewModels;

public class MyTemplateSelector : IDataTemplate
{
    public bool SupportsRecycling => false;
    [Content]
    public Dictionary<string, IDataTemplate> Templates {get;} = new Dictionary<string, IDataTemplate>();
    
    public bool Match(object? data)
    {
        return data is Gem;
    }

    public Control? Build(object? data)
    {
        if (data is Gem gem)
            return Templates[gem.ToString()].Build(data);
        else return null;
    }
}