using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Context;
using SocialFreeks.Entities;
using SocialFreeks.Repository.Core;

namespace Services
{
    public static class ServiceFactory
    {
        public static T Create<T>() where T : class
        {
            var type = typeof(T);
            var service = Activator.CreateInstance(type) as T;

            if (service == null)
                throw new NotSupportedException(String.Format("Could not create instance of {0}", type.FullName));

            return service;
        }

        public static void Bootstrap()
        {
            var repositoryAssembly = Assembly.Load("SocialFreeks.Repository");

            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(Settings.ConnectionString).ProxyFactoryFactory(typeof(ProxyFactoryFactory)))
                .ExposeConfiguration(GetContextConfiguration<WebSessionContext>)
                .Mappings(m => m.FluentMappings.AddFromAssembly(repositoryAssembly).Conventions.AddAssembly(repositoryAssembly))
                .BuildSessionFactory();

            SessionManager.Current = new SessionManager(sessionFactory);
        }

        public static void BindSession()
        {
            SessionManager.Current.Bind();
        }

        public static void UnBindSession()
        {
            SessionManager.Current.Unbind();
        }

        public static void DisposeSessionManager()
        {
            SessionManager.Current.Dispose();
        }

        private static void GetContextConfiguration<T>(NHibernate.Cfg.Configuration cfg)
        {
            var type = typeof(T);
            var contextClass = "call";

            if (type == typeof(WebSessionContext))
            {
                contextClass = "web";
            }
            else if (type == typeof(ThreadStaticSessionContext))
            {
                contextClass = "thread_static";
            }

            cfg.SetProperty("current_session_context_class", contextClass);
            cfg.SetProperty("command_timeout", "300");
        }
    }
}
