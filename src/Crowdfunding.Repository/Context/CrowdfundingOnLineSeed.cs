using System;
using System.Collections.Generic;
using System.Linq;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Context
{
    public class CrowdfundingOnLineSeed
    {
        public static void Seed(CrowdfundingOnlineDBContext context)
        {
            if (!context.Causes.Any())
            {
                var causes = new List<Cause> {
                    new Cause(Guid.NewGuid(), "Abrigo Sapeca", "Miracatu", "SP"),
                    new Cause(Guid.NewGuid(), "Projeto Lucianas", "Miracatu", "SP"),
                    new Cause(Guid.NewGuid(), "Associação Renascer", "Registro", "SP")
                };

                context.AddRange(causes);
                context.SaveChanges();
            }
        }
    }
}
