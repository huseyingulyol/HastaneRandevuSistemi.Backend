﻿namespace Application.Features.Feedbacks.Queries.GetListFeedback
{
    public class GetListFeedbackResponse
    {
        public string UserMail { get; set; }
        public string UserFeedback { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
