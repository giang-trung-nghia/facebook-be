﻿using Facebook.Domain.Entities.Conservation;
using Facebook.Domain.IRepositories.IConservation;
using Facebook.Infrastructure.Migrations.Contexts;
using Facebook.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Repositories.Conservation
{
    public class ConservationMemberRepository : BaseRepository<ConservationMemberEntity>, IConservationMemberRepository
    {
        private readonly AppDbContext _context;

        public ConservationMemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
