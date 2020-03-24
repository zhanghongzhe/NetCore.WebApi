using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Data.Domain.Parcels;
using NetCore.Services.Parcels;

namespace NetCore.WebApi.Controllers.V1
{
    [Route("api/v1/Parcel")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly ParcelService _parcelService;
        public ParcelController(ParcelService parcelService)
        {
            this._parcelService = parcelService;
        }

        [HttpGet("Index")]
        public Parcel Index()
        {
            return this._parcelService.Get(1111, "D026392934");
        }

        [HttpGet("GetByCustomer")]
        public IEnumerable<Parcel> GetByCustomer()
        {
            return this._parcelService.GetByCustomer(1111);
        }
    }
}