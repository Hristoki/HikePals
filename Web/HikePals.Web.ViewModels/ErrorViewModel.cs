﻿namespace HikePals.Web.ViewModels
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }

        public string RequestId { get; set; }

        public string RequestPath { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public bool ShowRequestPath => !string.IsNullOrEmpty(this.RequestPath);
    }
}
