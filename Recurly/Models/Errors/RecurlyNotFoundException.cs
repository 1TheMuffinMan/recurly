namespace Recurly
{
    /// <summary>
    /// The requested object is not defined in Recurly.
    /// </summary>
    public class RecurlyNotFoundException : RecurlyException
    {
        internal RecurlyNotFoundException(string message, Error[] errors)
            : base(message, errors)
        { }
    }
}