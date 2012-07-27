
using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class SupportTicketConversion
    {
        public static C.SupportTicket ToContract(this SupportTicket data)
        {
            if (data == null)
                return null;

            var model = new C.SupportTicket()
                {
                    TicketId = data.TicketId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CreatedByUserID = data.CreatedUserID,
                    CreatedDate = data.CreatedDate,
                    IssueText = data.IssueText,
                    SupportCategory = (SupportCategoryEnum)Enum.Parse(typeof(SupportCategoryEnum), data.IssueCategory),
                    ResolvedText = data.ResolvedText,
                    ResolvedDate = data.ResolvedDate,
                    ResolvedByUserID = data.ResolvedUserID,

                };

            return model;
        }

        public static SupportTicket ToDataModel(this C.SupportTicket model)
        {
            if (model == null) return null;

            var data = new SupportTicket()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedDate = DateTime.Today,
                CreatedUserID = model.CreatedByUserID,
                IssueCategory = model.SupportCategory.ToString(),
                IssueText = model.IssueText,
            };

            return data;
        }


        public static void ForUpdate(this SupportTicket existing, C.SupportTicket ticket)
        {
            existing.ResolvedUserID = ticket.ResolvedByUserID;
            existing.ResolvedText = ticket.ResolvedText;
            existing.ResolvedDate = DateTime.Today;
        }
    }
}
