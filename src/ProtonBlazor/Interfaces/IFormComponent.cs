// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Interfaces
{
    public interface IFormComponent
    {
        public bool Required { get; set; }
        public bool Error { get; set; }
        public bool HasErrors { get; }
        public bool Touched { get; }
        public object? Validation { get; set; }
        public bool IsForNull { get; }
        public List<string> ValidationErrors { get; set; }
        public Task ValidateAsync();
        public Task ResetAsync();
        public Task ResetValidationAsync();
    }
}
