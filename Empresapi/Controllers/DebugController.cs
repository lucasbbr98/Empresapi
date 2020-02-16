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
    using Utilities;
    using Services.Interfaces;
    using Models.Xml.ITR;
    using Models.Xml.FCA;
    using Models.Xml.DFP;
    using Models.Xml.FRE;
    using Models.Xml;
    using Constants;
    using System.Threading;
    using System.Net.Http;

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
        private readonly IDFPFinancialReportService dfpFinancialReportService;
        private readonly IFCACompanyIssuerService fcaCompanyIssuerService;
        private readonly IFRECompanyIntangibleService freCompanyIntangibleService;
        private readonly IFRECompanyOwnershipService freCompanyOwnershipService;
        private readonly IFRECompanyFixedAssetService freCompanyFixedAssetService;
        private readonly IFRECompanyAuditorService freCompanyAuditorService;
        private readonly IFRECompanyDebtService freCompanyDebtService;

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
            IDFPFinancialReportService dfpFinancialReportService,
            IFCACompanyIssuerService fcaCompanyIssuerService,
            IFRECompanyIntangibleService freCompanyIntangibleService,
            IFRECompanyOwnershipService freCompanyOwnershipService,
            IFRECompanyFixedAssetService freCompanyFixedAssetService,
            IFRECompanyAuditorService freCompanyAuditorService,
            IFRECompanyDebtService freCompanyDebtService
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
            this.dfpFinancialReportService = dfpFinancialReportService;
            this.fcaCompanyIssuerService = fcaCompanyIssuerService;
            this.freCompanyIntangibleService = freCompanyIntangibleService;
            this.freCompanyOwnershipService = freCompanyOwnershipService;
            this.freCompanyFixedAssetService = freCompanyFixedAssetService;
            this.freCompanyAuditorService = freCompanyAuditorService;
            this.freCompanyDebtService = freCompanyDebtService;
        }

        [HttpGet("dwqdwqdwqdwq")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetSourceScale()
        {
            try
            {
                var semaphore = new SemaphoreSlim(10, 50); 
                var sources = (await cvmSourceService.GetAll(showDeactivated: true)).ToList();
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();
                var i = 0;
                var lastId = sources.Last().Id;
                foreach (var s in sources)
                {
                    try
                    {
                        tasks.Add(Task.Run(async() => {
                            try
                            {
                                await semaphore.WaitAsync();
                                var content = await httpClientService.DownloadFile(s.Url);
                                var scale = XmlParser<Scale>.GetScale(content, s.Document);
                                s.CurrencyScale = scale.CurrencyScale;
                                s.QuantityScale = scale.QuantityScale;
                                var dbr = await cvmSourceService.Update(s);
                                if (dbr != HttpStatusCode.OK)
                                    Debug.Print($"Error: {s.Id}");
                                else
                                    Debug.Print($"Updated: {s.Id} out of {sources.Last().Id}");
                            }
                            catch(Exception e)
                            {
                                Debug.Print($"Error: {s.Id}");
                                errors.Add(s.Id);
                            }
                            finally
                            {
                                semaphore.Release();
                            }
                            
                        }));

                    }
                    catch (Exception ex)
                    {
                        Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                    }
                }


                Task.WaitAll(tasks.ToArray());

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

        [HttpGet("dwqdwqdqwxxx")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertCompanyIssuers()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Id = 19270 })).ToList();
                var done = await fcaCompanyIssuerService.GetAll();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                foreach(var s in sources)
                {
                    try
                    {
                        if (s.Id < 10554)
                            continue;

                        var content = await httpClientService.DownloadFile(s.Url);
                        var data = XmlParser<FCACompanyIssuer>.ParseElements(content);
                        if (!data.Any()) 
                            Debug.Print($"Null Data: {s.Id}");

                        foreach (var d in data)
                        {
                            d.ReferenceDate = s.ReferenceDate;
                            d.SourceId = s.Id;
                            d.CompanyId = s.CompanyId;
                            d.Cnpj = Formatter.CNPJ(d.Cnpj);
                            var dbr = await fcaCompanyIssuerService.Add(d);
                            if (dbr != HttpStatusCode.OK)
                                Debug.Print($"Error: {s.Id}");
                        }
                    }
                    catch (Exception ex)
                    {
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

        [HttpGet("aadwqdwqdqwxxx")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeactivateDuplicatedSourceVersion()
        {
            var i = 0;
            try
            {
                var companies = (await companyService.GetAll()).ToList();
                var len = companies.Count;
                foreach(var c in companies)
                {
                    i++;
                    Debug.Print($"{i} out of {len}");
                    var sources = (await cvmSourceService.GetAllWhere(new { Document = "FRE", CompanyId = c.Id })).ToList();
                    foreach(var s in sources)
                    {
                        var duplicates = sources.Where(x => 
                        x.ReferenceDate.Year == s.ReferenceDate.Year && 
                        x.ReferenceDate.Month == s.ReferenceDate.Month &&
                        x.ReferenceDate.Day == s.ReferenceDate.Day).ToList();
                        if (!duplicates.Any() || duplicates.Count <= 1)
                            continue;

                        
                        var mostRecent = duplicates.Max(x => x.SequenceNumber);
                        foreach(var d in duplicates)
                        {
                            if (d.SequenceNumber == mostRecent)
                                continue;

                            await cvmSourceService.Deactivate(d);
                        }
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

        [HttpGet("aadwqdwqdqdqwdwq")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteDuplicatedITRVersion()
        {
            try
            {
                
                var _sources = (await cvmSourceService.GetAllWhere(new { Document = "DFP" }, showDeactivated: true)).ToList();
                var sources = _sources.Where(s => s.DeactivatedOn != null).ToList();
                var n = 0;
                foreach (var s in sources)
                {
                    n++;
                    Debug.Print($"{s.Id} | {n} out of {sources.Count}");
                    var itrFR = await dfpFinancialReportService.GetAllWhere(new { SourceId = s.Id });
                    foreach (var i in itrFR)
                        await dfpFinancialReportService.Delete(i);

                    //var itrDIV = await itrDividendService.GetAllWhere(new { SourceId = s.Id });
                    //foreach (var i in itrDIV)
                    //    await itrDividendService.Delete(i);

                    //var itrCAP = await itrShareCapitalService.GetAllWhere(new { SourceId = s.Id });
                    //foreach (var i in itrCAP)
                    //    await itrShareCapitalService.Delete(i);

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

        [HttpGet("dwdqwqxxcc")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertITRDividends()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR" })).ToList();
                var doneDividends = await itrDividendService.GetAll();
                foreach (var d in doneDividends)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                var len = sources.Count;
                var i = 0;
                foreach(var s in sources)
                {
                    try
                    {
                        i++;
                        if (s.Id < 8700)
                            continue;
                        var content = await httpClientService.DownloadFile(s.Url);
                        var dividends = XmlParser<ITRDividend>.ParseElements(content);
                        if (!dividends.Any())
                            continue;

                        foreach(var d in dividends)
                        {
                            d.SourceId = s.Id;
                            d.CompanyId = s.CompanyId;
                            var dbr = await itrDividendService.Add(d);
                            if (dbr == HttpStatusCode.OK)
                                Debug.Print($"OK {s.Id} | {i} OUT OF {len}");
                            else
                                Debug.Print($"ERR {s.Id}");
                        }
                    }
                    catch(Exception ex)
                    {
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

        [HttpGet("dwqqqwqxxx")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertITRShareCapitals()
        {
            try
            {
               
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR" })).ToList();
                var done = await itrShareCapitalService.GetAll();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                var i = 0;
                var len = sources.Count;
                foreach (var s in sources)
                {
                    try
                    {
                        //TODO CHECK 2717
                        if (s.Id <= 2717)
                            continue;
                        i++;
                        Debug.Print($"{i} out of {len}");
                        var content = await httpClientService.DownloadFile(s.Url);
                        var data = XmlParser<ITRShareCapital>.ParseElements(content);
                        if (!data.Any()) { continue; }

                        foreach (var d in data)
                        {
                            d.SourceId = s.Id;
                            d.CompanyId = s.CompanyId;
                            d.ReferenceDate = s.ReferenceDate;
                            var dbr = await itrShareCapitalService.Add(d);
                            if (dbr != HttpStatusCode.OK)
                                Debug.Print($"ERROR {s.Id}");

                        }
                    }
                    catch (Exception ex)
                    {
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

        [HttpGet("wqdwdwefw")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertITRFinancialReports()
        {
            List<int> sourcesErrors = new List<int>();
            List<int> sourcesSuccess = new List<int>();
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "ITR"})).ToList();
                var done = await itrFinancialReportService.GetAll();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                var i = 0;
                var len = sources.Count;
                foreach (var s in sources)
                {
                    try
                    {
                        i++;
                        Debug.Print($"{i} OUT OF {len}");
                        var content = await httpClientService.DownloadFile(s.Url);
                        var data = XmlParser<ITRFinancialReport>.ParseElements(content);
                        data = data.Where(i => i != null && i != default(ITRFinancialReport)).ToList();
                        if (!data.Any()) 
                        {
                            sourcesErrors.Add(s.Id);
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
                Debug.Print($"ERROS: {sourcesErrors.ToString()}");
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("wqdwdwefw")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertDFPFinancialReports()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "DFP" })).ToList();
                var done = await dfpFinancialReportService.GetAll();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                var len = sources.Count;
                List<Task> tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(20, 80);
                List<int> errors = new List<int>();


                var last = sources.Last().Id;
                foreach (var s in sources)
                {
                    tasks.Add(Task.Run(async () => {
                        try
                        {
                            await semaphore.WaitAsync();
                            var content = await httpClientService.DownloadFile(s.Url);
                            var data = XmlParser<DFPFinancialReport>.ParseElements(content);
                            data = data.Where(i => i != null && i != default(DFPFinancialReport)).ToList();
                            if (!data.Any())
                                return;

                            foreach (var d in data)
                            {
                                try
                                {
                                    d.ReferenceDate = s.ReferenceDate;
                                    d.SourceId = s.Id;
                                    d.CompanyId = s.CompanyId;
                                    d.Year = d.ReferenceDate.Year;

                                    var dbr = await dfpFinancialReportService.Add(d);
                                    if (dbr != HttpStatusCode.OK)
                                        errors.Add(s.Id);

                                }
                                catch (ArgumentNullException a)
                                {
                                    errors.Add(s.Id);
                                    Debug.Print($"ERROR {s.Id} | Reason: {a.ToString()}");
                                }
                                catch (NotImplementedException n)
                                {
                                    errors.Add(s.Id);
                                    Debug.Print($"ERROR {s.Id} | Reason: {n.ToString()}");
                                }
                                catch (Exception e)
                                {
                                    errors.Add(s.Id);
                                    Debug.Print($"ERROR {s.Id} | Reason: {e.ToString()}");
                                }
                            }

                            Debug.Print($"Inserted {s.Id} OUT OF {last}");
                        }
                        catch (Exception ex)
                        {
                            errors.Add(s.Id);
                            Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
 

                }

                Task.WaitAll(tasks.ToArray());
                Debug.Print(errors.ToString());
                foreach(var e in errors)
                {
                    Debug.Print($"DELETE AND REDO SourceId {e.ToString()}");
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
        public async Task<IActionResult> InsertFRE()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "FRE" })).ToList();
                var done = await freCompanyAuditorService.GetAll();



                var len = sources.Count;
                List<Task> tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(20, 80);
                List<int> errors = new List<int>();
                List<int> mustDo = new List<int> { 44905, 44913, 44968, 44988, 45102, 45237, 46145, 46177, 46270, 46590, 48121, 48110, 48190, 48252, 48325, 48370, 48350, 48594, 48655 };
                var last = sources.Last().Id;
                foreach (var s in sources)
                {
                    if (!mustDo.Any(x => x == s.Id))
                        continue;

                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        var data = XmlParser<FRECompanyAuditor>.ParseElements(content);
                        data = data.Where(i => i != null && i != default(FRECompanyAuditor)).ToList();
                        if (!data.Any())
                            continue;

                        foreach (var d in data)
                        {
                            try
                            {
                                d.ReferenceDate = s.ReferenceDate;
                                d.SourceId = s.Id;
                                d.CompanyId = s.CompanyId;
                                d.Cnpj = Formatter.CNPJ(d.Cnpj);

                                var dbr = await freCompanyAuditorService.Add(d);
                                if (dbr != HttpStatusCode.OK)
                                    errors.Add(s.Id);

                            }
                            catch (ArgumentNullException a)
                            {
                                errors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {a.ToString()}");
                            }
                            catch (NotImplementedException n)
                            {
                                errors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {n.ToString()}");
                            }
                            catch (Exception e)
                            {
                                errors.Add(s.Id);
                                Debug.Print($"ERROR {s.Id} | Reason: {e.ToString()}");
                            }
                        }

                        Debug.Print($"Inserted {s.Id} OUT OF {last}");
                    }
                    catch (Exception ex)
                    {
                        errors.Add(s.Id);
                        Debug.Print($"ERROR {s.Id} | Reason: {ex.ToString()}");
                    }
                }

                Debug.Print(errors.ToString());
                foreach (var e in errors)
                {
                    Debug.Print($"DELETE AND REDO SourceId {e.ToString()}");
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