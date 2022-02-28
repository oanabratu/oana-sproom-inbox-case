﻿using Microsoft.EntityFrameworkCore;
using SproomInbox.API.Data.Entities;
using SproomInbox.API.Services;
using SproomInbox.Shared;

namespace SproomInbox.API.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _ctx;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public DocumentRepository(AppDbContext ctx)
        {
            _ctx = ctx;           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> CreateDocumentAsync(Document document)
        {
            var addedEntity = await _ctx.Documents.AddAsync(document);
            await _ctx.SaveChangesAsync();
            return addedEntity.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _ctx.Documents.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync(DocumentQueryParams queryParams)
        {
            return await _ctx.Documents
                .Include("DocumentStates")
                .Where(p =>
                (queryParams.Type == null || (int)p.DocumentType == queryParams.Type) &&
                (queryParams.State == null || (int)p.State == queryParams.State) &&
                (queryParams.Username == null || p.AssignedToUser == queryParams.Username)).ToListAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Document> GetDocumentById(Guid id)
        {
            return await _ctx.Documents
                .Include("DocumentStates")
                .FirstOrDefaultAsync(d => d.Id == id);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StateHistory>> GetDocumentHistory(Guid id)
        {
            var foundDocument = await _ctx.Documents
                .Include("DocumentStates")
                .FirstOrDefaultAsync(d => d.Id == id);

            if(foundDocument != null)
            {
                return foundDocument.DocumentStates.ToList();
            }

            return new List<StateHistory>();
        }
    }
}

