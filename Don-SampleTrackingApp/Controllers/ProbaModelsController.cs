using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Don_SampleTrackingApp;
using System.IO;
using OfficeOpenXml;

namespace Don_SampleTrackingApp.Controllers
{
    public class ProbaModelsController : Controller
    {
        private readonly RaportareDbContext _context;

        public ProbaModelsController(RaportareDbContext context)
        {
            _context = context;
        }

        // GET: ProbaModels
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.dataFrom = Auxiliar.GetOneMonthBeforeDate();
            ViewBag.dataTo = Auxiliar.GetTomorrowDate();
            List<ProbaModel> ListaSql = await _context.ProbaModels.ToListAsync();
            IEnumerable<ProbaModel> ListaDeAfisat = ListaSql.Where(item => Auxiliar.IsDateBetween(item.DataPrelevare, ViewBag.dataFrom, ViewBag.dataTo));
            return View(ListaDeAfisat);
        }

        // POST: ProbaModels
        // Afisam doar datele pe care le selecteaza utilizatorul sa le afiseze
        [HttpPost]
        public async Task<IActionResult> _Index(DateProbaDeAfisat selectieAfisareDate, string dataFrom, string dataTo)
        {
            if (dataFrom == null) dataFrom = Auxiliar.GetOneMonthBeforeDate();
            if (dataTo == null) dataTo = Auxiliar.GetTomorrowDate();
            IEnumerable<ProbaModel> ListaDeAfisat = await _context.ProbaModels.ToListAsync();
            switch (selectieAfisareDate)
            {
                // Afiseaza toate datele
                case DateProbaDeAfisat.Toate:
                    ListaDeAfisat = await _context.ProbaModels.ToListAsync();
                    break;
                // Afiseaza doar datele introduse de operator
                case DateProbaDeAfisat.ProbaIntrodusa:
                    ListaDeAfisat = _context.ProbaModels.Where(item => item.DataPreluare == "-" && item.DataRaspunsCalitate == "-");
                    break;
                // Afiseaza doar datele cu probele prelevate de operator calitate
                case DateProbaDeAfisat.ProbaPreluata:
                    ListaDeAfisat = _context.ProbaModels.Where(item => item.DataPreluare != "-" && item.DataRaspunsCalitate == "-");
                    break;
                // Afiseaza doar datele cu rezultat la calitate
                case DateProbaDeAfisat.ProbaRezultat:
                    ListaDeAfisat = _context.ProbaModels.Where(item => item.DataPreluare != "-" && item.DataRaspunsCalitate != "-");
                    break;
                // Afiseaza doar datele cu RNC
                case DateProbaDeAfisat.ProbaRNC:
                    ListaDeAfisat = _context.ProbaModels.Where(item => item.RezultatProba == Rezultat.RNC);
                    break;
                default:
                    break;
            }
            return PartialView(ListaDeAfisat.Where(item => Auxiliar.IsDateBetween(item.DataPrelevare, dataFrom, dataTo)));            
            // return View(await _context.ProbaModels.ToListAsync());
        }

        // GET: ProbaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaModel = await _context.ProbaModels
                .FirstOrDefaultAsync(m => m.ProbaModelId == id);
            if (probaModel == null)
            {
                return NotFound();
            }

            return View(probaModel);
        }

        // GET: ProbaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProbaModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProbaModelId,DataPrelevare,SiglaFurnizor,Sarja,Diametru,Calitate,NumarCuptor,TipTratamentTermic,TipCapBara,Tipproba,UserName,ObservatiiOperator,DataPreluare,DataRaspunsCalitate,NumeUtilizatorCalitate,RezultatProba,KV1,KV2,KV3,Temperatura,DuritateHB,ObservatiiCalitate")] ProbaModel probaModel)
        {
            if (ModelState.IsValid)
            {
                probaModel.DataPrelevare = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                _context.Add(probaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(probaModel);
        }

        // GET: ProbaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaModel = await _context.ProbaModels.FindAsync(id);
            if (probaModel == null)
            {
                return NotFound();
            }
            return View(probaModel);
        }

        // POST: ProbaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProbaModelId,DataPrelevare,SiglaFurnizor,Sarja,Diametru,Calitate,NumarCuptor,TipTratamentTermic,TipCapBara,Tipproba,UserName,ObservatiiOperator,DataPreluare,DataRaspunsCalitate,NumeUtilizatorCalitate,RezultatProba,KV1,KV2,KV3,Temperatura,DuritateHB,ObservatiiCalitate")] ProbaModel probaModel)
        {
            if (id != probaModel.ProbaModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbaModelExists(probaModel.ProbaModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(probaModel);
        }

        // GET: ProbaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaModel = await _context.ProbaModels
                .FirstOrDefaultAsync(m => m.ProbaModelId == id);
            if (probaModel == null)
            {
                return NotFound();
            }

            return View(probaModel);
        }

        // POST: ProbaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var probaModel = await _context.ProbaModels.FindAsync(id);
            _context.ProbaModels.Remove(probaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProbaModelExists(int id)
        {
            return _context.ProbaModels.Any(e => e.ProbaModelId == id);
        }


        /* Logica creare/ adaugare element in functie de rol
         * Rol Operator: Adauga doar o parte din element
         * Rol Calitate: Adauga restul de campuri din element (este practic o editare)
         * 
         */

        // Create Proba Operator
        // GET: ProbaModels/Create
        public IActionResult OperatorCreateProba()
        {
            return View();
        }

        // POST: ProbaModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperatorCreateProba([Bind("ProbaModelId,DataPrelevare,SiglaFurnizor,Sarja,Diametru,Calitate,NumarCuptor,TipTratamentTermic,TipCapBara,Tipproba,UserName,ObservatiiOperator,DataPreluare,DataRaspunsCalitate,NumeUtilizatorCalitate,RezultatProba,KV1,KV2,KV3,Temperatura,DuritateHB,ObservatiiCalitate")] ProbaModel probaModel)
        {
            if (ModelState.IsValid)
            {
                probaModel.DataPrelevare = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                _context.Add(probaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(probaModel);
        }

        // Create Rezultat proba Calitate (De fapt este un Edit la o proba deja existenta) 
        // GET: ProbaModels/Edit/5
        public async Task<IActionResult> OpCalitateCreateRezultat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probaModel = await _context.ProbaModels.FindAsync(id);
            if (probaModel == null)
            {
                return NotFound();
            }
            return View(probaModel);
        }

        // POST: ProbaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OpCalitateCreateRezultat(int id, [Bind("ProbaModelId,DataPrelevare,SiglaFurnizor,Sarja,Diametru,Calitate,NumarCuptor,TipTratamentTermic,TipCapBara,Tipproba,UserName,ObservatiiOperator,DataPreluare,DataRaspunsCalitate,NumeUtilizatorCalitate,RezultatProba,KV1,KV2,KV3,Temperatura,DuritateHB,ObservatiiCalitate")] ProbaModel probaModel)
        {
            if (id != probaModel.ProbaModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    probaModel.DataRaspunsCalitate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    _context.Update(probaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProbaModelExists(probaModel.ProbaModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(probaModel);
        }


        // Functie exportare data to excel file
        public async Task<IActionResult> ExportToExcelAsync(string dataFrom, string dataTo)
        {
            //return Content(dataFrom + "<==>" + dataTo);
            List<ProbaModel> listaExcel = await _context.ProbaModels.ToListAsync();
            
            // Extrage datele cuprinse intre limitele date de operator
            IEnumerable<ProbaModel> listaDeAfisat = listaExcel.Where(model => Auxiliar.IsDateBetween(model.DataPrelevare, dataFrom, dataTo));

            var stream = new MemoryStream();

            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Cuptor");
                ws.Cells["A1:Z1"].Style.Font.Bold = true;

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Data prelevare";
                ws.Cells["C1"].Value = "Sigla furnizor";
                ws.Cells["D1"].Value = "Sarja";
                ws.Cells["E1"].Value = "Diametru";
                ws.Cells["F1"].Value = "Calitate";
                ws.Cells["G1"].Value = "Nr cuptor";
                ws.Cells["H1"].Value = "Tip Tratament Termic";
                ws.Cells["I1"].Value = "Cap bara";
                ws.Cells["J1"].Value = "Tip proba";
                ws.Cells["K1"].Value = "User name";
                ws.Cells["L1"].Value = "Obs operator";
                ws.Cells["M1"].Value = "Data preluare";
                ws.Cells["N1"].Value = "Data raspuns";
                ws.Cells["O1"].Value = "User name calitate";
                ws.Cells["P1"].Value = "Rezultat proba";
                ws.Cells["Q1"].Value = "KV1";
                ws.Cells["R1"].Value = "KV2";
                ws.Cells["S1"].Value = "KV3";
                ws.Cells["T1"].Value = "Temperatura";
                ws.Cells["U1"].Value = "Duritate HB";
                ws.Cells["V1"].Value = "Obs Calitate";

                int rowStart = 2;
                foreach (var elem in listaDeAfisat)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = elem.ProbaModelId;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = elem.DataPrelevare;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = elem.SiglaFurnizor;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = elem.Sarja;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = elem.Diametru;
                    ws.Cells[string.Format("F{0}", rowStart)].Value = elem.Calitate;
                    ws.Cells[string.Format("G{0}", rowStart)].Value = elem.NumarCuptor;
                    ws.Cells[string.Format("H{0}", rowStart)].Value = elem.TipTratamentTermic;
                    ws.Cells[string.Format("I{0}", rowStart)].Value = elem.TipCapBara;
                    ws.Cells[string.Format("J{0}", rowStart)].Value = elem.Tipproba;
                    ws.Cells[string.Format("K{0}", rowStart)].Value = elem.UserName;
                    ws.Cells[string.Format("L{0}", rowStart)].Value = elem.ObservatiiOperator;
                    ws.Cells[string.Format("M{0}", rowStart)].Value = elem.DataPreluare;
                    ws.Cells[string.Format("N{0}", rowStart)].Value = elem.DataRaspunsCalitate;
                    ws.Cells[string.Format("O{0}", rowStart)].Value = elem.NumeUtilizatorCalitate;
                    ws.Cells[string.Format("P{0}", rowStart)].Value = elem.RezultatProba;
                    ws.Cells[string.Format("Q{0}", rowStart)].Value = elem.KV1;
                    ws.Cells[string.Format("R{0}", rowStart)].Value = elem.KV2;
                    ws.Cells[string.Format("S{0}", rowStart)].Value = elem.KV3;
                    ws.Cells[string.Format("T{0}", rowStart)].Value = elem.Temperatura;
                    ws.Cells[string.Format("U{0}", rowStart)].Value = elem.DuritateHB;
                    ws.Cells[string.Format("V{0}", rowStart)].Value = elem.ObservatiiCalitate;

                    rowStart++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();

                pck.Save();
            }
            stream.Position = 0;
            string excelName = "RaportGazCuptor.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }

    }
}
