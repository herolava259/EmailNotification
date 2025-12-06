using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Console.TryThenLearn.OpenAI;


public record OpenAISettingBase(string ApiKey) { }

public record OpenAIModelSetting(string ApiKey, string ModelId): OpenAISettingBase(ApiKey)
{ }

public record OpenAIEmbeddingSetting(string ApiKey, string ModelEmbeddingId): OpenAISettingBase(ApiKey)
{ }
public sealed class OpenAISettings
{
    public string ApiKey { get; set; } = String.Empty;

    public string ModelId { get; set; } = String.Empty;


    public double Temperature { get; set; } = 1.0;

    public int MaxTokens { get; set; } = -1;
}
