using Soenneker.Blazor.Utils.LocalStorage.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

[Collection("Collection")]
public sealed class LocalStorageUtilTests : FixturedUnitTest
{
    private readonly ILocalStorageUtil _blazorlibrary;

    public LocalStorageUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _blazorlibrary = Resolve<ILocalStorageUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
