# \# Projektnapló – Konzolos rajzoló

# 

# \## Csapat és munkamegosztás

# 

# | milkaa2000 | Alapstruktúra, menürendszer, kurzormozgatás, szín- és karakterválasztás, állapotjelzés | Cella törlése (4-es menüpont) |





# | mazsumarcell | Nyomdák (F1–F12) megvalósítása, fájlbetöltés (`Betoltes`), hibakezelés | Ugrás középre (5-ös menüpont) |

# 

# \## Közös megállapodások

# 

# \- A rajzlap mérete és helye: A rajzlap 52 oszlop széles és 20 sor magas (`SZEL = 52`, `MAG = 20`). A bal oldali 23 karakteres menü- és állapotpanel után, a 24. oszloptól és a 2. sortól kezdődik, így a logikai `(x, y)` rajzlap-koordináta a konzol `(24 + x, 1 + y)` pozíciójára esik.





# \- A két munkarész kapcsolódási pontja: A `rajz\[y, x]` és `rajzSzin\[y, x]` kétdimenziós tömbök, valamint a `Beir()`, `Hiba()` és `Rajzol()` közös segédfüggvények, amelyekre mind a szabadkézi rajzolás, mind a nyomdák és a fájlbetöltés épül.





# \- A módosítások egyeztetése a main ágon: A közös `Program.cs` fájlt egymás után, előzetes egyeztetés mellett módosítjuk. Minden munkamenet előtt `git pull` paranccsal frissítjük a helyi másolatot, és csak működő, leforduló kódot töltünk fel.

# 

# \## Munkanapló

# 

# | Dátum | Ki | Mi készült el vagy miben állapodtunk meg? | Következő lépés vagy commit |





# | 2026. 11. 12. | milkaa2000 | Megállapodtunk a képernyőfelosztásban (23 + 1 + 52 oszlop) és elkészült a kezdőprojekt az ablakméret-ellenőrzéssel. | 

# 

# `Alapstruktúra, konzol-ellenőrzések és rajzlap kirajzolása` |

# | 2026. 11. 12. | milkaa2000 | Elkészült a nyilakkal történő kurzormozgatás (`Lep`), a szóközös eszközváltás (`Beir`) és a szélellenőrzés hangjelzéssel (`Hang`, `Hiba`). | `Kurzor mozgatása, rajzolás és hibajelzés hozzáadása` |





# | 2026. 11. 12. | mazsumarcell | Elkészültek az F1–F12 nyomdák (X, kereszt, négyzetkeret, rombuszkeret 3 méretben) a kirajzolás előtti határellenőrzéssel. | 

# `Nyomda funkció megvalósítása (F1-F12)` |





# | 2026. 11. 12. | milkaa2000 | Elkészült az állapotgép alapú menürendszer (`mod` változó), valamint a karakter- és színválasztó almenü. | `Menürendszer, szín- és karakterválasztás implementálása` |





# | 2026. 11. 12. | mazsumarcell | Elkészült a `minta.txt` (és tetszőleges fájl) beolvasása méret- és létezés-ellenőrzéssel (`Betoltes`). | `Minta betöltése fájlból (Betoltes függvény)` |





# | 2026. 11. 12. | Közös | Beépítettük a két saját menüfunkciót: az aktuális cella törlését (4) és a középpontba ugrást (5). | `Saját funkciók hozzáadása: cella törlése és ugrás középre` |

# 

# \## Másik pár tesztje

# 

# \- Tesztelt commit: \[A tesztelt commit 7 jegyű azonosítója, pl. `a1b2c3d`]

# \- Visszajelzés és saját javítás: \[A másik pár visszajelzése alapján elvégzett módosítások, vagy: A tesztelők minden funkciót hibátlannak találtak, külön kiemelték a határellenőrzés pontos működését.]

# \- Újrateszt eredménye: \[Minden teszteset megfelelt.]

# 

# \## Otthoni AI-használat

# 

# \- Név: milkaa2000





# \- Miben kértem segítséget: A konzolos alkalmazás elindításának lépéseiben, a Git-commitok logikus felbontásában, valamint a submodul- és push-hibák elhárításában kértem segítséget.





# \- Mit tanultam meg: Megértettem, hogyan működik a `Console.SetCursorPosition` koordináta-rendszere és a konzolméret ellenőrzése, valamint megtanultam a tiszta Git-munkamenet lépéseit (`git add`, `git commit`, `git push`) és a belső `.git` mappák okozta hibák kezelését.

