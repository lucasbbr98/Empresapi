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
    using System.Threading;
    using System.IO;

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
        private readonly IFRECompanyShareholderService freCompanyShareholderService;
        private readonly IFRECompanyCapitalDistributionService freCompanyCapitalDistributionService;
        private readonly IFRECompanyRelatedTransactionService freCompanyRelatedTransactionService;
        private readonly IFRECompanyShareBuybackService freCompanyShareBuybackService;
        private readonly IFRECompanyTreasuryActionService freCompanyTreasuryActionService;
        private readonly IFRECompanyAdministratorService freCompanyAdministratorService;
        private readonly IFRECompanyCommitteeMemberService freCompanyCommitteeMemberService;
        private readonly IFRECompanyFamilyRelationshipService freCompanyFamilyRelationshipService;
        private readonly IFRECompanySubordinateRelationshipService freCompanySubordinateRelationshipService;
        private readonly IFRECompanyBoardCompensationService freCompanyBoardCompensationService;
        private readonly IFRECompanyAdministrationCompensationService freCompanyAdministrationCompensationService;
        private readonly IFRECompanyCapitalIncreaseService freCompanyCapitalIncreaseService;
        private readonly IFRECompanyCapitalEventService freCompanyCapitalEventService;
        private readonly IFRECompanyCapitalReductionService freCompanyCapitalReductionService;



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
            IFRECompanyDebtService freCompanyDebtService,
            IFRECompanyShareholderService freCompanyShareholderService,
            IFRECompanyCapitalDistributionService freCompanyCapitalDistributionService,
            IFRECompanyRelatedTransactionService freCompanyRelatedTransactionService,
            IFRECompanyShareBuybackService freCompanyShareBuybackService,
            IFRECompanyTreasuryActionService freCompanyTreasuryActionService,
            IFRECompanyAdministratorService freCompanyAdministratorService,
            IFRECompanyCommitteeMemberService freCompanyCommitteeMemberService,
            IFRECompanyFamilyRelationshipService freCompanyFamilyRelationshipService,
            IFRECompanySubordinateRelationshipService freCompanySubordinateRelationshipService,
            IFRECompanyBoardCompensationService freCompanyBoardCompensationService,
            IFRECompanyAdministrationCompensationService freCompanyAdministrationCompensationService,
            IFRECompanyCapitalIncreaseService freCompanyCapitalIncreaseService,
            IFRECompanyCapitalEventService freCompanyCapitalEventService,
            IFRECompanyCapitalReductionService freCompanyCapitalReductionService
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
            this.freCompanyShareholderService = freCompanyShareholderService;
            this.freCompanyCapitalDistributionService = freCompanyCapitalDistributionService;
            this.freCompanyRelatedTransactionService = freCompanyRelatedTransactionService;
            this.freCompanyShareBuybackService = freCompanyShareBuybackService;
            this.freCompanyTreasuryActionService = freCompanyTreasuryActionService;
            this.freCompanyAdministratorService = freCompanyAdministratorService;
            this.freCompanyCommitteeMemberService = freCompanyCommitteeMemberService;
            this.freCompanyFamilyRelationshipService = freCompanyFamilyRelationshipService;
            this.freCompanySubordinateRelationshipService = freCompanySubordinateRelationshipService;
            this.freCompanyBoardCompensationService = freCompanyBoardCompensationService;
            this.freCompanyAdministrationCompensationService = freCompanyAdministrationCompensationService;
            this.freCompanyCapitalIncreaseService = freCompanyCapitalIncreaseService;
            this.freCompanyCapitalEventService = freCompanyCapitalEventService;
            this.freCompanyCapitalReductionService = freCompanyCapitalReductionService;
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

        [HttpGet("dwdqwdqw")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetFiles()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "FRE" })).ToList();
                List<Task> tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(20, 80);
                List<int> errors = new List<int>();
                List<int> toDo = new List<int> { 38855, 38859, 38862, 38875, 41649, 41655, 41654, 41692, 41693, 41697, 41701, 45154, 45158 };

                var last = sources.Last().Id;
                foreach (var s in sources)
                {
                    if (!toDo.Any(x => s.Id == x))
                        continue;

                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        SaveStreamAsFile(content, $"{s.SequenceNumber}.zip");
                        Debug.Print($"Done {s.Id} out of {last}");
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

        public static void SaveStreamAsFile(Stream inputStream, string fileName, string filePath = "D:\\lucascvm")
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }


        [HttpGet("dwqdqwwqdwq")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertFRE()
        {
            try
            {
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "FRE" })).ToList();
                var done = await freCompanyCapitalDistributionService.GetAll();


                var len = sources.Count;
                List<Task> tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(20, 80);
                List<int> errors = new List<int>();
                List<int> toDo = new List<int> {44276, 44345, 47769, 48406 };
                foreach(var i in toDo)
                {
                    var zList = await freCompanyCapitalDistributionService.GetAllWhere(new { SourceId = i});
                    foreach (var z in zList)
                        await freCompanyCapitalDistributionService.Delete(z);

                }

                var last = sources.Last().Id;
                foreach (var s in sources)
                {
                    if (!toDo.Any(x => x == s.Id))
                        continue;
                    try
                    {
                        var content = await httpClientService.DownloadFile(s.Url);
                        var data = XmlParser<FRECompanyCapitalDistribution>.ParseElements(content);
                        data = data.Where(i => i != null && i != default(FRECompanyCapitalDistribution)).ToList();
                        if (!data.Any())
                            continue;


                        foreach (var d in data)
                        {
                            try
                            {
                                d.ReferenceDate = s.ReferenceDate;
                                d.SourceId = s.Id;
                                d.CompanyId = s.CompanyId;

                                var dbr = await freCompanyCapitalDistributionService.Add(d);
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

        [HttpGet("dwqdqaawwqdwq")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> InsertFREFromHD()
        {
            try
            {
                List<Task> tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(20, 80);
                var sources = (await cvmSourceService.GetAllWhere(new { Document = "FRE" })).ToList();
                var done = await freCompanyCapitalReductionService.GetAll();
                foreach (var d in done)
                    sources.RemoveAll(x => x.Id == d.SourceId);

                var len = sources.Count;
                List<int> errors = new List<int>();

                var last = sources.Last().Id;
                foreach (var s in sources)
                {
                    try
                    {
                        Stream content = System.IO.File.OpenRead(@$"D:\lucascvm\{s.SequenceNumber}.zip");
                        var data = XmlParser<FRECompanyCapitalReduction>.ParseElements(content);
                        data = data.Where(i => i != null && i != default(FRECompanyCapitalReduction)).ToList();
                        if (!data.Any())
                            continue;

                        foreach (var d in data)
                        {

                            try
                            {
                                d.ReferenceDate = s.ReferenceDate;
                                d.SourceId = s.Id;
                                d.CompanyId = s.CompanyId;

                                var dbr = freCompanyCapitalReductionService.Add(d);

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
                    Debug.Print($"DELETE AND REDO SourceId {e.ToString()}");


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
        public async Task<IActionResult> BacenSGS()
        {   //http://api.bcb.gov.br/dados/serie/bcdata.sgs.20749/dados?formato=csv&dataInicial=01/01/2010&dataFinal=31/12/2016
            var docCode = 432;
            var initialDate = new DateTime(year: 2019, month: 1, day: 1).ToString("dd/MM/yyyy");
            var finalDate = new DateTime(year: 2019, month: 12, day: 31).ToString("dd/MM/yyyy");
            var url = $"http://api.bcb.gov.br/dados/serie/bcdata.sgs.{docCode}/dados?formato=json&dataInicial={initialDate}&dataFinal={finalDate}";
            var json = await httpClientService.Get(url);

            return Ok();
        }

    }
}