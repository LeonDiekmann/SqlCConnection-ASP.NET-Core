﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Helper
{

    public class RepoException<T> : Exception
    {
        public T Type { get; set; }
        public RepoException(T type)
        {
            Type = type;
        }
        public RepoException(string message, T type) : base(message)
        {
            Type = type;
        }
        public RepoException(string message, Exception inner) : base(message, inner) { }
        protected RepoException(
            System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public enum UpdateResultType
    {
        OK,
        SQLERROR,
        NOTFOUND,
        INVALIDEARGUMENT,
        ERROR
    }
}
