using System;

namespace SocialFreeks.Database
{
    public class MigrationState
    {
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
        public Exception Exception { get; set; }
    }
}
