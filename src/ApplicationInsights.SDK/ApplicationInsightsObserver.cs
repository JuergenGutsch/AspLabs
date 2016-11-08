﻿using System;
using System.Diagnostics;
using ApplicationInsights.Listener;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights
{
    public class ApplicationInsightsObserver : IObserver<DiagnosticListener>
    {
        private readonly AspNetCoreHostingListener _aspNetCoreHostingListener;
        private readonly SystemNetHttpListener _systemNetHttpListener;    
        private readonly EntityFrameworkListener _entityFrameworkListener;
        private readonly SqlClientListener _sqlClientListener;

        public ApplicationInsightsObserver(TelemetryClient client)
        {
            _aspNetCoreHostingListener = new AspNetCoreHostingListener(client);
            _systemNetHttpListener = new SystemNetHttpListener(client);
            _entityFrameworkListener = new EntityFrameworkListener(client);
            _sqlClientListener = new SqlClientListener(client);
        }

        public void OnCompleted()
        {
            return;
        }

        public void OnError(Exception error)
        {
            return;
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value.Name == "Microsoft.AspNetCore")
            {
                value.SubscribeWithAdapter(_aspNetCoreHostingListener);
            }
            if (value.Name == "HttpHandlerDiagnosticListener")
            {
                value.SubscribeWithAdapter(_systemNetHttpListener);
            }
            if (value.Name == "Microsoft.EntityFrameworkCore")
            {
                value.SubscribeWithAdapter(_entityFrameworkListener);
            }
            if (value.Name == "SqlClientDiagnosticListener")
            {
                value.SubscribeWithAdapter(_sqlClientListener);
            }
        }
    }
}
