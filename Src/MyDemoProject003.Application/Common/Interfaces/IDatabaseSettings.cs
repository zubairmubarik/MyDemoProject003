﻿namespace MyDemoProject003.Application.Common.Interfaces
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
