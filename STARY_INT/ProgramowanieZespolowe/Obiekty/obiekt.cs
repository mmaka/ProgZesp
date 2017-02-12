using System.Windows.Forms;

namespace dodawanie_figur1
{
    abstract class Obiekt
    {
        protected int x, y;
        protected int w, h;
        protected int vx, vy;
        protected int id;
        protected string name;

      //  public float X { get { return x; } }
      //  public float Y { get { return y; } }
        public int W { get { return w; } }
        public int H { get { return h; } }
        public int VX { get { return vx; } }
        public int VY { get { return vy; } }
        public int ID { get { return id; } }

        public Obiekt() { }

        public Obiekt(int x1, int y1, int w1, int h1,int id, int vx1 = 0, int vy1 = 0, string nm="Obiekt")
        {
            this.name = nm;
            this.id = id;
            this.x = x1;
            this.y = y1;
            this.w = w1;
            this.h = h1;
            this.vx = vx1;
            this.vy = vy1;
        }



        public virtual void rysuj(object sender, PaintEventArgs e) { }
        public virtual void rysujOB(object sender, PaintEventArgs e) { }
        public virtual string Name { get { return name; } }

        public virtual void move(int x, int y) { }

    }
}