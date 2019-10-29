using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Don_SampleTrackingApp;

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
            return View(await _context.ProbaModels.ToListAsync());
        }

        // POST: ProbaModels
        // Afisam doar datele pe care le selecteaza utilizatorul sa le afiseze
        [HttpPost]
        public async Task<IActionResult> _Index(DateProbaDeAfisat selectieAfisareDate)
        {
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
            return PartialView(ListaDeAfisat);            
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

    }
}
