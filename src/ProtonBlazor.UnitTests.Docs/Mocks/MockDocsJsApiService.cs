using ProtonBlazor.Docs.Services;

namespace ProtonBlazor.UnitTests.Mocks;

public class MockDocsJsApiService : IDocsJsApiService
{
    public ValueTask<string> GetInnerTextByIdAsync(string id) => ValueTask.FromResult("inner text");
}
