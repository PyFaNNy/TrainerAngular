using MediatR;

namespace Trainer.Application.Abstractions;

public class GetPaginatedListBaseQuery<T> : IRequest<T>
{
    public int PageIndex
    {
        get; protected set;
    }

    public int PageSize
    {
        get; protected set;
    }

    public int Skip
    {
        get
        {
            return PageSize * (PageIndex - 1);
        }
    }

    protected GetPaginatedListBaseQuery(int? pageIndex, int? pageSize)
    {
        PageIndex = GetValue(pageIndex,1);
        PageSize = GetValue(pageSize, 10);
    }

    private int GetValue(int? value, int defaultValue)
    {
        return value == null || value < 1 ? defaultValue : value.Value;
    }
}
