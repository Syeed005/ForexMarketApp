﻿using ForexMarket.Data;
using ForexMarket.Repository.IRepository;

namespace ForexMarket.Repository {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext db;
        public ICreditApplicationRepository CreditApplication { get; private set; }

        public UnitOfWork(ApplicationDbContext db) {
            this.db = db;
            CreditApplication = new CreditApplicationRepository(this.db);
        }
        public void Dispose() {
            db.Dispose();
        }

        public void Save() {
            db.SaveChanges();
        }
    }
}
