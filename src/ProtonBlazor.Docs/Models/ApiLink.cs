using ProtonBlazor.Charts;
namespace ProtonBlazor.Docs.Models
{
#nullable enable
    public static class ApiLink
    {
        /// <summary>
        /// Gets the URL of the API documentation for a type.
        /// </summary>
        /// <param name="type">The type to find.</param>
        public static string GetApiLinkFor(Type type)
        {
            return $"api/{type.Name.Replace("`1", "").Replace("`2", "").ToLowerInvariant()}";
        }

        public static string GetComponentLinkFor(Type type)
        {
            return $"components/{GetComponentName(type)}";
        }

        /// <summary>
        /// Converts a lowercase component name from an URL into the C# Type name.
        /// Examples: 
        ///   table --> <see cref="ProTable{T}"/>
        ///   button  <see cref="ProButton"/>
        ///   appbar  <see cref="ProAppBar"/>
        /// </summary>
        public static Type? GetTypeFromComponentLink(string component)
        {
            if (string.IsNullOrEmpty(component))
            {
                return null;
            }
            if (component.Contains('#'))
            {
                component = component[..component.IndexOf('#')];
            }
            if (InverseSpecialCase.TryGetValue(component, out var type))
            {
                return type;
            }

            var assembly = typeof(ProComponentBase).Assembly;
            foreach (var componentType in assembly.GetTypes())
            {
                var typeNameWithoutGenericInfo = new string(componentType.Name.ToLowerInvariant().TakeWhile(c => c != '`').ToArray());
                if (typeNameWithoutGenericInfo.Equals($"mud{component}", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (componentType.Name.Contains('`'))
                    {
                        return componentType.MakeGenericType(typeof(T));
                    }

                    if (string.Equals(componentType.Name, $"mud{component}", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return componentType;
                    }
                }
            }

            return null;
        }

        private static string GetComponentName(Type type)
        {
            if (!SpecialCaseComponents.TryGetValue(type, out var component))
            {
                component = new string(type
                        .ToString()
                        .Replace("ProtonBlazor.Mud", "")
                        .TakeWhile(c => c != '`')
                        .ToArray())
                    .ToLowerInvariant();
            }

            return component;
        }

        private static readonly Dictionary<Type, string> SpecialCaseComponents =
            new()
            {
                [typeof(ProFab)] = "buttonfab",
                [typeof(ProIcon)] = "icons",
                [typeof(ProProgressCircular)] = "progress",
                [typeof(ProText)] = "typography",
                [typeof(ProSnackbarProvider)] = "snackbar",
                [typeof(Bar<T>)] = "barchart",
                [typeof(StackedBar<T>)] = "stackedbarchart",
                [typeof(Donut<T>)] = "donutchart",
                [typeof(Line<T>)] = "linechart",
                [typeof(TimeSeries<T>)] = "timeserieschart",
                [typeof(ScatterPlot<T>)] = "scatterplotchart",
                [typeof(Pie<T>)] = "piechart",
                [typeof(ProChip<T>)] = "chips",
                [typeof(ChartOptions)] = "options"
            };

        // this is the inversion of above lookup
        private static readonly Dictionary<string, Type> InverseSpecialCase =
            SpecialCaseComponents.ToDictionary(pair => pair.Value, pair => pair.Key);
    }
}
