using Soenneker.Blazor.Utils.LocalStorage.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class LocalStorageUtilTests : HostedUnitTest
{
    private readonly ILocalStorageUtil _blazorlibrary;

    public LocalStorageUtilTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<ILocalStorageUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
