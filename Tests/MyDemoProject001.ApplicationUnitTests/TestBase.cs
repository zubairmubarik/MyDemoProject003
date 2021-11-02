using NUnit.Framework;
using System.Threading.Tasks;

namespace MyDemoProject003.ApplicationUnitTests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
