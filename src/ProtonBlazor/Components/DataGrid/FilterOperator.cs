// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using ProtonBlazor.Resources;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents comparison operations which execute a filter in a <see cref="ProDataGrid{T}"/>.
    /// </summary>
    public static class FilterOperator
    {
        /// <summary>
        /// Represents filters which are available for <c>string</c> values.
        /// </summary>
        /// <remarks>
        /// You can control case sensitivity of filters by setting the <see cref="ProDataGrid{T}.FilterCaseSensitivity"/> property.
        /// </remarks>
        public static class String
        {
            /// <summary>
            /// Find text containing the filter value.
            /// </summary>
            public const string Contains = "contains";

            /// <summary>
            /// Find text which does not contain the filter value.
            /// </summary>
            public const string NotContains = "not contains";

            /// <summary>
            /// Find text which is the same as the filter value.
            /// </summary>
            public const string Equal = "equals";

            /// <summary>
            /// Find text which is different from the filter value.
            /// </summary>
            public const string NotEqual = "not equals";

            /// <summary>
            /// Find text which starts with the filter value.
            /// </summary>
            public const string StartsWith = "starts with";

            /// <summary>
            /// Find text which ends with the filter value.
            /// </summary>
            public const string EndsWith = "ends with";

            /// <summary>
            /// Find text which is null, empty, or whitespace.
            /// </summary>
            public const string Empty = "is empty";

            /// <summary>
            /// Find text which is not null, empty, or whitespace.
            /// </summary>
            public const string NotEmpty = "is not empty";
        }

        /// <summary>
        /// Represents filters which are available for numeric values.
        /// </summary>
        /// <remarks>
        /// Numeric filters support all numeric types, including <c>int</c>, <c>double</c>, <c>decimal</c>, <c>long</c>, <c>short</c>, <c>sbyte</c>, <c>byte</c>, <c>ulong</c>, <c>ushort</c>, <c>uint</c>, <c>float</c> and <c>BigInteger</c>.
        /// </remarks>
        public static class Number
        {
            /// <summary>
            /// Find numbers equal to the filter value.
            /// </summary>
            public const string Equal = "=";

            /// <summary>
            /// Find numbers different from the filter value.
            /// </summary>
            public const string NotEqual = "!=";

            /// <summary>
            /// Find numbers larger than the filter value.
            /// </summary>
            public const string GreaterThan = ">";

            /// <summary>
            /// Find numbers larger than, or equal to, the filter value.
            /// </summary>
            public const string GreaterThanOrEqual = ">=";

            /// <summary>
            /// Find numbers smaller than the filter value.
            /// </summary>
            public const string LessThan = "<";

            /// <summary>
            /// Find numbers smaller than, or equal to, the filter value.
            /// </summary>
            public const string LessThanOrEqual = "<=";

            /// <summary>
            /// Find null values.
            /// </summary>
            public const string Empty = "is empty";

            /// <summary>
            /// Find values which are not null.
            /// </summary>
            public const string NotEmpty = "is not empty";
        }

        /// <summary>
        /// Represents filters which are available for enumerations.
        /// </summary>
        public static class Enum
        {
            /// <summary>
            /// Find values matching the filter value.
            /// </summary>
            public const string Is = "is";

            /// <summary>
            /// Find values which do not match the filter value.
            /// </summary>
            public const string IsNot = "is not";

            /// <summary>
            /// Find rows where the nullable enum column is <c>null</c>.
            /// </summary>
            public const string Empty = "is empty";

            /// <summary>
            /// Find rows where the nullable enum column is not <c>null</c>.
            /// </summary>
            public const string NotEmpty = "is not empty";
        }

        /// <summary>
        /// Represents filters which are available for boolean values.
        /// </summary>
        public static class Boolean
        {
            /// <summary>
            /// Find values which match the filter value.
            /// </summary>
            public const string Is = "is";
        }

        /// <summary>
        /// Represents filters which are available for date and time values.
        /// </summary>
        public static class DateTime
        {
            /// <summary>
            /// Find values matching the filter date.
            /// </summary>
            public const string Is = "is";

            /// <summary>
            /// Find values different from the filter date.
            /// </summary>
            public const string IsNot = "is not";

            /// <summary>
            /// Find values after the filter date.
            /// </summary>
            public const string After = "is after";

            /// <summary>
            /// Find values on or after the filter date.
            /// </summary>
            public const string OnOrAfter = "is on or after";

            /// <summary>
            /// Find values before the filter date.
            /// </summary>
            public const string Before = "is before";

            /// <summary>
            /// Find values on or before the filter date.
            /// </summary>
            public const string OnOrBefore = "is on or before";

            /// <summary>
            /// Find null values.
            /// </summary>
            public const string Empty = "is empty";

            /// <summary>
            /// Find any non-null value.
            /// </summary>
            public const string NotEmpty = "is not empty";
        }

        /// <summary>
        /// Represents filters which are available for date only values.
        /// </summary>
        public static class DateOnly
        {
            /// <summary>
            /// Find values matching the filter date.
            /// </summary>
            public const string Is = "is";

            /// <summary>
            /// Find values different from the filter date.
            /// </summary>
            public const string IsNot = "is not";

            /// <summary>
            /// Find values after the filter date.
            /// </summary>
            public const string After = "is after";

            /// <summary>
            /// Find values on or after the filter date.
            /// </summary>
            public const string OnOrAfter = "is on or after";

            /// <summary>
            /// Find values before the filter date.
            /// </summary>
            public const string Before = "is before";

            /// <summary>
            /// Find values on or before the filter date.
            /// </summary>
            public const string OnOrBefore = "is on or before";

            /// <summary>
            /// Find null values.
            /// </summary>
            public const string Empty = "is empty";

            /// <summary>
            /// Find any non-null value.
            /// </summary>
            public const string NotEmpty = "is not empty";
        }

        /// <summary>
        /// Represents filters which are available for Guid values.
        /// </summary>
        public static class Guid
        {
            /// <summary>
            /// Find values matching the filter Guid.
            /// </summary>
            public const string Equal = "equals";

            /// <summary>
            /// Find values different from the filter Guid.
            /// </summary>
            public const string NotEqual = "not equals";
        }

        internal static string[] GetOperatorByDataType(Type? type)
        {
            var fieldType = FieldType.Identify(type);
            return GetOperatorByDataType(fieldType);
        }

        internal static string[] GetOperatorByDataType(FieldType fieldType)
        {
            if (fieldType.IsString)
            {
                return new[]
                {
                    String.Contains,
                    String.NotContains,
                    String.Equal,
                    String.NotEqual,
                    String.StartsWith,
                    String.EndsWith,
                    String.Empty,
                    String.NotEmpty,
                };
            }
            if (fieldType.IsNumber)
            {
                return new[]
                {
                    Number.Equal,
                    Number.NotEqual,
                    Number.GreaterThan,
                    Number.GreaterThanOrEqual,
                    Number.LessThan,
                    Number.LessThanOrEqual,
                    Number.Empty,
                    Number.NotEmpty,
                };
            }
            if (fieldType.IsEnum)
            {
                return new[] {
                    Enum.Is,
                    Enum.IsNot,
                    Enum.Empty,
                    Enum.NotEmpty,
                };
            }
            if (fieldType.IsBoolean)
            {
                return new[]
                {
                    Boolean.Is,
                };
            }
            if (fieldType.IsDateOnly)
            {
                return new[]
                {
                    DateOnly.Is,
                    DateOnly.IsNot,
                    DateOnly.After,
                    DateOnly.OnOrAfter,
                    DateOnly.Before,
                    DateOnly.OnOrBefore,
                    DateOnly.Empty,
                    DateOnly.NotEmpty,
                };
            }
            if (fieldType.IsDateTime)
            {
                return new[]
                {
                    DateTime.Is,
                    DateTime.IsNot,
                    DateTime.After,
                    DateTime.OnOrAfter,
                    DateTime.Before,
                    DateTime.OnOrBefore,
                    DateTime.Empty,
                    DateTime.NotEmpty,
                };
            }
            if (fieldType.IsGuid)
            {
                return new[]
                {
                    Guid.Equal,
                    Guid.NotEqual,
                };
            }

            // default
            return Array.Empty<string>();
        }

        internal static string GetTranslationKeyByOperatorName(string operatorName) => operatorName switch
        {
            // All these operator constants should be refactored to be enums, this is a temporary solution.
            // The commented lines are duplicate constants.
            String.Contains => LanguageResource.ProDataGrid_Contains,
            String.NotContains => LanguageResource.ProDataGrid_NotContains,
            String.Equal => LanguageResource.ProDataGrid_Equals,
            String.NotEqual => LanguageResource.ProDataGrid_NotEquals,
            String.StartsWith => LanguageResource.ProDataGrid_StartsWith,
            String.EndsWith => LanguageResource.ProDataGrid_EndsWith,
            String.Empty => LanguageResource.ProDataGrid_IsEmpty,
            String.NotEmpty => LanguageResource.ProDataGrid_IsNotEmpty,
            Number.Equal => LanguageResource.ProDataGrid_EqualSign,
            Number.NotEqual => LanguageResource.ProDataGrid_NotEqualSign,
            Number.GreaterThan => LanguageResource.ProDataGrid_GreaterThanSign,
            Number.GreaterThanOrEqual => LanguageResource.ProDataGrid_GreaterThanOrEqualSign,
            Number.LessThan => LanguageResource.ProDataGrid_LessThanSign,
            Number.LessThanOrEqual => LanguageResource.ProDataGrid_LessThanOrEqualSign,
            //Number.Empty => LanguageResource.ProDataGrid_IsEmpty,
            //Number.NotEmpty => LanguageResource.ProDataGrid_IsNotEmpty,
            Enum.Is => LanguageResource.ProDataGrid_Is,
            Enum.IsNot => LanguageResource.ProDataGrid_IsNot,
            //Boolean.Is => LanguageResource.ProDataGrid_Is,
            //DateTime.Is => LanguageResource.ProDataGrid_Is,
            //DateTime.IsNot => LanguageResource.ProDataGrid_IsNot,
            DateTime.After => LanguageResource.ProDataGrid_IsAfter,
            DateTime.OnOrAfter => LanguageResource.ProDataGrid_IsOnOrAfter,
            DateTime.Before => LanguageResource.ProDataGrid_IsBefore,
            DateTime.OnOrBefore => LanguageResource.ProDataGrid_IsOnOrBefore,
            //DateTime.Empty => LanguageResource.ProDataGrid_IsEmpty,
            //DateTime.NotEmpty => LanguageResource.ProDataGrid_IsNotEmpty,
            //Guid.Equal => LanguageResource.ProDataGrid_Equals,
            //Guid.NotEqual => LanguageResource.ProDataGrid_NotEquals,
            _ => throw new ArgumentOutOfRangeException(nameof(operatorName), operatorName, "Unknown operator name.")
        };
    }
}
