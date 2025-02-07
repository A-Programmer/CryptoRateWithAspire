namespace Project.Api.ViewModels.CryptoCurrencyViewModels;

public class ResultStatus : ResultMessage
{
    public string CssClass { get; set; }
    public object? Data { get; set; }

    public ResultStatus(bool isSuccess, Status status, string message, string cssClass, object? data = null)
        : base(isSuccess, status, message)
    {
        CssClass = cssClass;
        Data = data;
    }

    public void Update(bool isSuccess, Status status, string message, string cssClass, object? data = null)
    {
        IsSuccess = isSuccess;
        Status = status;
        Message = message;
        CssClass = cssClass;
        Data = data;
    }
}
