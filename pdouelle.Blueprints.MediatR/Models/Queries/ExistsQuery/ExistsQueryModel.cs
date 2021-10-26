using System.Collections.Generic;
using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Queries.ExistsQuery
{
    public class ExistsQueryModel<TResource> : IRequest<bool>
    {
        public List<KeyValuePair<string, object>> KeyValues { get; set; }
        
        public ExistsQueryModel(List<KeyValuePair<string, object>> keyValues)
        {
            KeyValues = keyValues;
        }
    }
}