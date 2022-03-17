    export function kreiranjeKontejnera()
    {
        var host = document.body;
        
        var kontejner=document.createElement("div");
        kontejner.className="glavniKontejner";
        host.appendChild(kontejner);

        var leviKontejner=document.createElement("div");
        leviKontejner.className="leviKontejner";
        kontejner.appendChild(leviKontejner);

        var desniKontejner=document.createElement("div");
        desniKontejner.className="desniKontejner";
        kontejner.appendChild(desniKontejner);

        var desnaStrana=document.createElement("div");
        desnaStrana.className="desnaStrana";
        desniKontejner.appendChild(desnaStrana);

        var desnaKartica=document.createElement("div");
        desnaKartica.className="desnaKartica";
        desnaStrana.appendChild(desnaKartica);

        return leviKontejner;
    }

  
  
