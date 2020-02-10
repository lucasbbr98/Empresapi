using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Empresapi.Controllers
{
    using Constants;
    using Utilities;
    using Services.Interfaces;
    using Models.Xml.ITR;
    using Models.Xml.FCA;

    public class DebugController : BaseController
    {
        private readonly IConfiguration config;
        private readonly ILogger<DebugController> logger;
        private readonly IHttpClientService httpClientService;

        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly ICVMSourceService cvmSourceService;
        private readonly IITRDividendService itrDividendService;
        private readonly IITRShareCapitalService itrShareCapitalService;
        private readonly IITRFinancialReportService itrFinancialReportService;
        private readonly IFCACompanySecurityService fcaCompanySecurityService;


        public DebugController(
            ILogger<DebugController> logger,
            IConfiguration config,
            IUserService userService,
            ICompanyService companyService,
            ICVMSourceService cvmSourceService,
            IHttpClientService httpClientService,
            IITRDividendService itrDividendService,
            IITRShareCapitalService itrShareCapitalService,
            IITRFinancialReportService itrFinancialReportService,
            IFCACompanySecurityService fcaCompanySecurityService
            )
        {
            this.logger = logger;
            this.config = config;
            this.userService = userService;
            this.companyService = companyService;
            this.cvmSourceService = cvmSourceService;
            this.httpClientService = httpClientService;
            this.itrDividendService = itrDividendService;
            this.itrShareCapitalService = itrShareCapitalService;
            this.itrFinancialReportService = itrFinancialReportService;
            this.fcaCompanySecurityService = fcaCompanySecurityService;
        }


        [HttpGet("insert/div")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertITRDividends()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR" })).ToList();
                var doneDividends = await itrDividendService.GetAll();
                List<int> sourcesErrors = new List<int>();
                List<int> sourcesSuccess = new List<int>();
                foreach (var d in doneDividends)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                foreach(var s in sources)
                {
                    if (sourcesSuccess.Any(x => x == s.Id))
                        continue;

                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        var xml = CVMUnzipper.OpenFile(content, CVMFileExtension.ITR, CVMFile.ITRDividends);
                        if (string.IsNullOrEmpty(xml))
                            continue;

                        var dividends = new XmlParser<ITRDividend>().ParseElements(xml);
                        if (!dividends.Any()) { sourcesSuccess.Add(s.Id); continue; }

                        foreach(var d in dividends)
                        {
                            d.SourceId = s.Id;
                            d.CompanyId = s.CompanyId;
                            var dbr = await itrDividendService.Add(d);
                            if (dbr != HttpStatusCode.OK)
                                sourcesErrors.Add(s.Id);
                            else
                                sourcesSuccess.Add(s.Id);
                        }
                    }
                    catch(Exception ex)
                    {
                        sourcesErrors.Add(s.Id);
                        Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                    }

                }

                return Ok(new 
                { 
                    Code = 200,
                    Message = "OK"
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }

        }

        [HttpGet("")]
        public async Task<IActionResult> InsertITRShareCapitals()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR" })).ToList();
                var done = await itrShareCapitalService.GetAll();
                List<int> sourcesErrors = new List<int>();
                List<int> sourcesSuccess = new List<int>();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                foreach (var s in sources)
                {
                    if (sourcesSuccess.Any(x => x == s.Id))
                        continue;

                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        var xml = CVMUnzipper.OpenFile(content, CVMFileExtension.ITR, CVMFile.ITRShareCapital);
                        if (string.IsNullOrEmpty(xml))
                            continue;

                        var data = new XmlParser<ITRShareCapital>().ParseElements(xml);
                        if (!data.Any()) { sourcesSuccess.Add(s.Id); continue; }

                        foreach (var d in data)
                        {
                            d.SourceId = s.Id;
                            d.CompanyId = s.CompanyId;
                            d.ReferenceDate = s.ReferenceDate;
                            var dbr = await itrShareCapitalService.Add(d);
                            if (dbr != HttpStatusCode.OK)
                                sourcesErrors.Add(s.Id);
                            else
                                sourcesSuccess.Add(s.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        sourcesErrors.Add(s.Id);
                        Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                    }

                }

                return Ok(new
                {
                    Code = 200,
                    Message = "OK"
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("update/share/capitals")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateITRShareCapitals()
        {
            var shareCapitals = await itrShareCapitalService.GetAll();
            foreach(var s in shareCapitals)
            {
                try
                {
                    var source = await cvmSourceService.Get(s.SourceId);
                    if (source == null)
                        throw new NullReferenceException();

                    s.ReferenceDate = source.ReferenceDate;
                    await itrShareCapitalService.Update(s);
                }
                catch (Exception e)
                {
                    Debug.Print("WTF");
                }
            }

            return Ok();

        }

        [HttpGet("kdmkwqmdwqmd")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertITRFinancialReports()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR"})).ToList();
                var done = await itrFinancialReportService.GetAll();
                List<int> sourcesErrors = new List<int>();
                List<int> sourcesSuccess = new List<int>();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                foreach (var s in sources)
                {
                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        var xml = CVMUnzipper.OpenFile(content, CVMFileExtension.ITR, CVMFile.ITRFinancialReports);
                        if (string.IsNullOrEmpty(xml))
                            continue;

                        var data = new XmlParser<ITRFinancialReport>().ParseElements(xml);
                        data = data.Where(i => i != null && i != default(ITRFinancialReport)).ToList();
                        if (!data.Any()) 
                        { 
                            sourcesSuccess.Add(s.Id); 
                            continue; 
                        }


                        foreach (var d in data)
                        {
                            try
                            {
                                d.ReferenceDate = s.ReferenceDate;
                                d.SourceId = s.Id;
                                d.CompanyId = s.CompanyId;
                                var dbr = await itrFinancialReportService.Add(d);
                                if (dbr != HttpStatusCode.OK)
                                    sourcesErrors.Add(s.Id);
                                else
                                    sourcesSuccess.Add(s.Id);
                            }
                            catch (ArgumentNullException a)
                            {
                                sourcesErrors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {a.ToString()}");
                            }
                            catch (NotImplementedException n)
                            {
                                sourcesErrors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {n.ToString()}");
                            }
                            catch (Exception e)
                            {
                                sourcesErrors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {e.ToString()}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sourcesErrors.Add(s.Id);
                        Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                    }

                }

                return Ok(new
                {
                    Code = 200,
                    Message = "OK"
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

    }
}