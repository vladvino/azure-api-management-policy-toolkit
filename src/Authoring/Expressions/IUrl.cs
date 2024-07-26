namespace Mielek.Azure.ApiManagement.PolicyToolkit.Authoring.Expressions;
public interface IUrl
{
    string Host { get; }
    string Path { get; }
    string Port { get; }
    IReadOnlyDictionary<string, string[]> Query { get; }
    string QueryString { get; }
    string Scheme { get; }
}