using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtonBlazor.Docs.Models
{
    public class DocsComponents
    {
        private readonly List<ProComponent> _proComponents = new();

        public DocsComponents AddItem(string name, Type component, params Type[] childComponents)
        {
            var componentItem = new ProComponent
            {
                Name = name,
                Link = name.ToLowerInvariant().Replace(" ", ""),
                Type = component,
                ChildTypes = childComponents,
                IsNavGroup = false
            };
            _proComponents.Add(componentItem);

            return this;
        }

        public DocsComponents AddNavGroup(string name, bool expanded, DocsComponents groupItems)
        {
            var componentItem = new ProComponent
            {
                Name = name,
                NavGroupExpanded = expanded,
                GroupComponents = groupItems.GetComponentsSortedByName(),
                IsNavGroup = true
            };
            _proComponents.Add(componentItem);

            return this;
        }

        internal List<ProComponent> Components => _proComponents;

        internal List<ProComponent> GetComponentsSortedByName()
        {
            return _proComponents.OrderBy(e => e.Name).ToList();
        }
    }
}
