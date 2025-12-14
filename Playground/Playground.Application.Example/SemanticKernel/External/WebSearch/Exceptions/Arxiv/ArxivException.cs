using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SemanticKernel.External.WebSearch.Exceptions.Arxiv;

public class ArxivException: Exception
{
    protected readonly string _url;

    protected readonly int _retry;

    protected readonly string _message;

    public ArxivException(string url, int retry, string message): base(message)
    {
        _url = url;
        _retry = retry;
        _message = message;
    }


    public override string ToString()
    {
        return $"{this._message} ({this._url}) \n {base.ToString()}";
    }
}


public class UnexpectedEmptyPageException: ArxivException
{
    private readonly string rawFeedback;

    public UnexpectedEmptyPageException(string url, int retry, string _rawFeedback): base(url, retry, "Page of results was unexpectedly empty")
    {
        this.rawFeedback = _rawFeedback;
    }

    public override string ToString()
    {
        return $"{nameof(UnexpectedEmptyPageException)} ({base._url}, {base._retry}, {this.rawFeedback})" + base.ToString();
    }
}


public class HttpArxivException : ArxivException
{
    private readonly int status;

    public HttpArxivException(string url, int retry, int status) : base(url, retry, $"Page request resulted in HTTP {status}")
    {
        this.status = status;
    }


    public override string ToString()
    {
        return $"{nameof(HttpArxivException)}({base._url}, {base._retry}, {this.status})";
    }
}
