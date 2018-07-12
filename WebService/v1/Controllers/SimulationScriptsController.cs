// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.Diagnostics;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Exceptions;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Filters;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Models;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Models.DeviceModelApiModel;

namespace Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Controllers
{
    [ExceptionsFilter]
    public class SimulationScriptsController : Controller
    {
        private readonly ILogger log;

        public SimulationScriptsController(
            ILogger logger)
        {
            this.log = logger;
        }

        [HttpGet(Version.PATH + "/[controller]")]
        public string GetAsync()
        {
            return "Status: OK";
        }

        //[HttpPost(Version.PATH + "/[controller]!validate")]
        //public ActionResult Validate(IFormFile file)
        //{
        //    // TODO: add verification helper
        //    //dynamic checkResults = await VerificationHelpers.CheckScriptFileAsync(appFile, log);
        //    if (file == null)
        //    {
        //        return new JsonResult(new DeviceModelApiValidation()) { StatusCode = (int)HttpStatusCode.BadRequest };
        //    }

        //    return new JsonResult(new DeviceModelApiValidation()) { StatusCode = (int)HttpStatusCode.OK };
        //}
    }
}
