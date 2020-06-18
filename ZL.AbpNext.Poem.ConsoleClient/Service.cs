﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using ZL.AbpNext.Poem.Core.Poems;

namespace ZL.AbpNext.Poem.ConsoleClient
{
    public class Service : ITransientDependency
    {
        IRepository<Poet> repository;
        IUnitOfWorkManager uowManager;
        public Service(IRepository<Poet> repository, IUnitOfWorkManager uowManager)
        {
            this.repository = repository;
            this.uowManager = uowManager;
        }
        public void Run()
        {
            //Console.WriteLine("你好");
            using (var uow = uowManager.Begin(new AbpUnitOfWorkOptions()))
            {
                //获取第一个诗人
                //var poet = repository.FirstOrDefault();
                var poet = repository.AsQueryable().Include(p => p.Poems).FirstOrDefault();
                Console.WriteLine(poet.Name);
                Console.WriteLine(poet.Poems.Count());
                Console.WriteLine(poet.Poems.ToList()[0].Author.Name);
            }
        }
    }
}
