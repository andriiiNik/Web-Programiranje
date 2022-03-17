import { Butik } from "./Butik.js";
import { Grad } from "./Grad.js"
import { kreiranjeKontejnera } from "./LanacButika.js";

fetch("https://localhost:5001/Butik/VratiListuGradovaiButika")
.then(p=>{
    if(p.ok)
    {
        p.json().then(data=>{
            var leviKontejner=kreiranjeKontejnera();
            data.forEach(grad => {
                var g=new Grad(grad.id,grad.nazivGrada);
                grad.gradAdrese.forEach(butici=>{
                    var b=new Butik(butici.butik.id,butici.butik.nazivButika,butici.butik.telefon,butici.adresa,grad.id);
                    g.listaButika.push(b);
                });
                g.crtaj(leviKontejner);
            });
        })
    }
})



