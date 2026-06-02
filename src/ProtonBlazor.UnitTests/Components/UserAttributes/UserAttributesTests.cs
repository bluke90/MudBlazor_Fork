// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using ProtonBlazor.Charts;
using ProtonBlazor.Services;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.UserAttributes
{
    [TestFixture]
    public sealed class UserAttributesTests
    {
        static UserAttributesTests()
        {
            Exclude(typeof(ProBooleanInput<>)); // This is the base class of Switch and CheckBox and should be skipped
            Exclude(typeof(ProHidden));         // No need to test
            Exclude(typeof(ProBreakpointProvider)); // just exposing a cascading value, no layout implications
            Exclude(typeof(ProPicker<>));       // Internal component, skip
            Exclude(typeof(ProRadioGroup<>));   // Wrapping component, skip
            Exclude(typeof(ProDragHandle<>));   // Wrapping component, skip
            Exclude(typeof(ProOverlay));        // Sectioned component, skip
            Exclude(typeof(DataGridGroupRow<>));  // Internal component, skip
            Exclude(typeof(DataGridVirtualizeRow<>)); // Internal component, skip
            Exclude(typeof(BaseRadialChart<,>)); // Internal component, skip
            Exclude(typeof(BaseAxisChart<,>)); // Internal component, skip
        }

        [Test]
        public async Task AllMudComponents_ShouldForwardUserAttributes()
        {
            // Arrange
            await using var testContext = new BunitContext();
            testContext.AddTestServices();
            testContext.Services.Add(new ServiceDescriptor(typeof(IResizeObserver), new MockResizeObserver()));

            var componentFactory = new ProComponentFactory
            {
                UserAttributes = new Dictionary<string, object> { { "data-testid", "test-123" } },
            };

            // Act & Assert
            var mudComponentTypes = GetMudComponentTypes();

            mudComponentTypes.Should().NotBeEmpty();

            // these components do not need to have user attributes
            var excludedComponents = new HashSet<string>()
            {
                nameof(ProPopover), nameof(ProStep), nameof(ProContextualActionBar), nameof(ProHotkey), nameof(ProExitPrompt),
                "Column`1", "FooterCell`1", "HeaderCell`1", "FilterHeaderCell`1", "SelectColumn`1",
                "HierarchyColumn`1", "PropertyColumn`2", "TemplateColumn`1", "ProToggleItem`1", "ProHeatMapCell`1"
            };

            foreach (var componentType in mudComponentTypes)
            {
                if (excludedComponents.Contains(componentType.Name))
                {
                    continue;
                }

                var component = componentFactory.Create(componentType, testContext);
                component.Markup.Should()
                    .NotBeEmpty(because: $"the component {componentType.Name} should at least contain one element");

                var elementsWithUserAttributes = component.FindAll("[data-testid='test-123']");
                elementsWithUserAttributes.Should()
                    .NotBeEmpty(because: $"UserAttributes should be forwarded by component {componentType.Name}");
            }
        }

        private Type[] GetMudComponentTypes()
        {
            return typeof(ProElement).Assembly
                .GetTypes()
                .Where(type => type.IsAssignableTo(typeof(ProComponentBase)) && !type.IsAbstract)
                .Select(type => type.IsGenericType ? type.GetGenericTypeDefinition() : type)
                .Except(_excludedComponents)
                .ToArray();
        }

        private static ConcurrentBag<Type> _excludedComponents = [];
        private static void Exclude(Type componentType) => _excludedComponents.Add(componentType);
    }
}
