namespace test;

using static Constants;

using Mielek.Expressions.Testing;
using Mielek.Expressions.Testing.Mocks;
using Newtonsoft.Json.Linq;

[TestClass]
public class FilterBodyExpressionTest
{
    static Expression expression;

    [ClassInitialize]
    public static void Init(TestContext c)
    {
        expression = ExpressionProvider.LoadFromFile(ScriptPath("filter-body.csx"));
    }

    [ClassCleanup]
    public static void Cleanup()
    {
        expression = null;
    }

    [TestMethod]
    public async Task Test()
    {
        var context = new MockContext();
        context.MockResponse.MockBody.Content = "{ \"current\": \"some current content\", \"other\": \"some other content\" }";

        var result = await expression.Execute(context);

        var actual = JObject.Parse(result);
        var expected = JObject.Parse("{ \"other\": \"some other content\" }");

        Assert.IsTrue(JObject.DeepEquals(actual, expected));
    }

}