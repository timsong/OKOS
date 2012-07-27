using WFS.Repository;

namespace WFS.Framework
{
    public static class ResponseExtensions
    {

		public static void Merge(this IResponse originalResponse, IResponse newResponse)
        {
            originalResponse.Status = MergeStatus(originalResponse.Status, newResponse.Status);
            originalResponse.Messages.AddRange(newResponse.Messages);
        }
		public static void Merge<T1>(this Result<T1> originalResponse, IResponse newResponse)
		{
			originalResponse.Status = MergeStatus(originalResponse.Status, newResponse.Status);
			originalResponse.Messages.AddRange(newResponse.Messages);
		}

		public static void Merge<T1,T2>(this Result<T1> originalResponse, Result<T2> newResponse)
		{
			originalResponse.Status = MergeStatus(originalResponse.Status, newResponse.Status);
			originalResponse.Messages.AddRange(newResponse.Messages);
		}

        public static void Merge(this IResponse originalResponse, IResult result)
        {
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Messages);
        }

        public static void Merge(this IListResponse originalResponse, IListResponse newResponse)
        {
            originalResponse.Status = MergeStatus(originalResponse.Status, newResponse.Status);
            originalResponse.Messages.AddRange(newResponse.Messages);

            originalResponse.PageSize = newResponse.PageSize;
            originalResponse.PageIndex = newResponse.PageIndex;
            originalResponse.Total = newResponse.Total;
        }

        public static void Merge(this IListResponse originalResponse, IListResult result)
        {
            originalResponse.Status = MergeStatus(originalResponse.Status, result.Status);
            originalResponse.Messages.AddRange(result.Messages);

            originalResponse.PageIndex = result.PageIndex;
            originalResponse.PageSize = result.PageSize;
            originalResponse.Total = result.Total;
        }


        private static Status MergeStatus(Status original, Status result)
        {
            switch (original)
            {
                case Status.Undefined:
                case Status.Success:
                    return result;
                case Status.Warning:
                    if (result == Status.Error)
                        return result;
                    break;
                case Status.Error:
                    //do nothing, never revert an error
                    break;
            }

            return original;
        }
    }

}
