using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windowsToast
{
    public partial class toastWindow : Form
    {
        //estructura para el color de las notificaciones
        public struct toastColor
        {
            public Color _background;
            public Color _border;
            public Color _title;
            public Color _subtitle;
            
        }
        //aca estan definidas las combinaciones de colores, se pueden agregar mas
        //pero tambien hay que agregarlas al enum de clases posibles
        //la definicion de los colores esta en InitializeColors()
        private toastColor _toastPrimary;
        private toastColor _toastSecondary;
        private toastColor _toastSuccess;
        private toastColor _toastDanger;
        private toastColor _toastWarning;
        private toastColor _toastInfo;
        private toastColor _toastLight;
        private toastColor _toastDark;

        //enum de clases posibles (es para llamar a la notif por parametros, asi hay combinaciones "predefinidas" para el parametro,
        //y mas adelante se arma la combinacion de colores con estos valores "predefinidos")
        public enum toastClass
        {
            Primary,
            Secondary,
            Success,
            Danger,
            Warning,
            Info,
            Light,
            Dark

        }

        //variables de resguardo locales para lo que vino por parametros
        private string _idNotificacion = ""; //aca se va a guardar el id de la notificacion (puede servir para cuando se hace click)
        private toastClass _clase; //aca se va a guardar la clase de la notif (el color)
        private int _time; //aca se va a guardar el tiempo que se muestra la notif antes de su "fade-out"
        private bool _clicked = false; //es para saber si se esta haciendo click sobre la notificacion

        /// <summary>
        /// Inicializar la notificacion con los datos a mostrar
        /// </summary>
        /// <param name="idNotificacion">El ID de la notificacion</param>
        /// <param name="titulo">El titulo de la notificacion</param>
        /// <param name="subtitulo">El subtitulo de la notificacion</param>
        /// <param name="clase">La clase (color) de la notificacion</param>
        /// <param name="showTime">Cuanto tiempo se muestra, en milisegundos (0 para que no haga fade-out)</param>
        public toastWindow(string idNotificacion, string titulo, string subtitulo, toastClass clase,int showTime)
        {
            InitializeComponent();
            //resguardo los valores en las variables locales
            _idNotificacion = idNotificacion;
            _clase = clase;
            _time = showTime;
            //armo las combinaciones de colores
            initializeColors();
            //pongo el titulo y el subtitulo en los labels
            lblTitle.Text = titulo;
            lblSubtitle.Text = subtitulo;
            //llamo al metodo para "armar" la notificacion
            showForm();
        }

        //este metodo se ejecuta cuando se hace un click en la notificacion
        //aca tiene que ir el codigo para hacer lo que se quiere hacer al clickear
        private void respuesta()
        {
            //HACER ALGO CUANDO SE HACE CLICK EN LA NOTIFICACION
            this.Close();
        }

        //metodo para saber en que lugar del monitor mostrar la notificacion, acomodar controles y cargar el fade-out
        private void showForm()
        {
            //contar cuantas notificaciones hay abiertas ya
            int instancias = 0;
            try
            {
                for (int i = 0; i < Application.OpenForms.Count; i++) //para cada form abierto del programa
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "toastWindow") //si el form se llama "toastWindow", contar
                    {
                        instancias++;
                    }
                }
            }
            catch
            {
                instancias = 0;
            }

            //posicionar el form en la pantalla, funciona como un sistema cartesiano de coordenadas X Y, en donde
            // el punto x=0,y=0 es la esquina superior izquierda del monitor
            // info: http://www.e-cartouche.ch/content_reg/cartouche/graphics/en/html/Screen_learningObject3.html

            //la posicion respecto al borde izquierdo del monitor (X):
            //ancho del monitor-ancho de notif-10 (lo pone del lado derecho del monitor)

            //la posicion respecto al borde superior del monitor (Y):
            //(alto de notif*cantidad de notif)+(separacion de cada notif*cant de notif)+separacion adicional
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) - 10, ((instancias* this.Height)+(instancias*5)) + 15);
            
            //posicionar el picturebox que funciona como fondo/borde
            pictureBox1.Width = this.Width - 2;
            pictureBox1.Height = this.Height - 2;
            pictureBox1.Left = 1;
            pictureBox1.Top = 1;


            //tiempo para autocerrado
            if (_time>0)
            {
                timer1.Interval = _time;
                timer1.Enabled = true;
            }
        }

        //aca estan definidas las combinaciones de colores
        private void initializeColors()
        {
            //primary
            _toastPrimary._background = Color.FromArgb(204, 229, 255);
            _toastPrimary._border = Color.FromArgb(184, 218, 255);
            _toastPrimary._title = Color.FromArgb(0, 64, 133);
            _toastPrimary._subtitle = Color.FromArgb(129, 129, 130);
            //secondary
            _toastSecondary._background = Color.FromArgb(226, 227, 229);
            _toastSecondary._border = Color.FromArgb(214, 216, 219);
            _toastSecondary._title = Color.FromArgb(56, 61, 65);
            _toastSecondary._subtitle = Color.FromArgb(56, 61, 65);
            //success
            _toastSuccess._background = Color.FromArgb(212, 237, 218);
            _toastSuccess._border = Color.FromArgb(195, 230, 203);
            _toastSuccess._title = Color.FromArgb(21, 87, 36);
            _toastSuccess._subtitle = Color.FromArgb(129, 129, 130);
            //danger
            _toastDanger._background = Color.FromArgb(248, 215, 218);
            _toastDanger._border = Color.FromArgb(245, 198, 203);
            _toastDanger._title = Color.FromArgb(114, 28, 36);
            _toastDanger._subtitle = Color.FromArgb(129, 129, 130);
            //warning
            _toastWarning._background = Color.FromArgb(255, 243, 205);
            _toastWarning._border = Color.FromArgb(255,238,186);
            _toastWarning._title = Color.FromArgb(133,100,4);
            _toastWarning._subtitle = Color.FromArgb(129, 129, 130);
            //info
            _toastInfo._background = Color.FromArgb(209,236,241);
            _toastInfo._border = Color.FromArgb(190,229,235);
            _toastInfo._title = Color.FromArgb(12,84,96);
            _toastInfo._subtitle = Color.FromArgb(129, 129, 130);
            //light
            _toastLight._background = Color.FromArgb(254,254,254);
            _toastLight._border = Color.FromArgb(198,200,202);
            _toastLight._title = Color.FromArgb(129,129,130);
            _toastLight._subtitle = Color.FromArgb(129, 129, 130);
            //dark
            _toastDark._background = Color.FromArgb(214,216,217);
            _toastDark._border = Color.FromArgb(198, 200, 202);
            _toastDark._title = Color.FromArgb(27,30,33);
            _toastDark._subtitle = Color.FromArgb(129, 129, 130);

        }

        //esto es para ponerle los colores a la notificacion cuando se muestra el form
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            switch (_clase)
            {
                case toastClass.Primary:
                    pictureBox1.BackColor = _toastPrimary._background;
                    this.BackColor = _toastPrimary._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastPrimary._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastPrimary._title;
                    lblSubtitle.ForeColor = _toastPrimary._subtitle;
                    lblTitle.BackColor = _toastPrimary._background;
                    lblSubtitle.BackColor = _toastPrimary._background;
                    break;

                case toastClass.Secondary:
                    pictureBox1.BackColor = _toastSecondary._background;
                    this.BackColor = _toastSecondary._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastSecondary._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastSecondary._title;
                    lblSubtitle.ForeColor = _toastSecondary._subtitle;
                    lblTitle.BackColor = _toastSecondary._background;
                    lblSubtitle.BackColor = _toastSecondary._background;
                    break;

                case toastClass.Success:
                    pictureBox1.BackColor = _toastSuccess._background;
                    this.BackColor = _toastSuccess._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastSuccess._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastSuccess._title;
                    lblSubtitle.ForeColor = _toastSuccess._subtitle;
                    lblTitle.BackColor = _toastSuccess._background;
                    lblSubtitle.BackColor = _toastSuccess._background;
                    break;

                case toastClass.Danger:
                    pictureBox1.BackColor = _toastDanger._background;
                    this.BackColor = _toastDanger._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastDanger._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastDanger._title;
                    lblSubtitle.ForeColor = _toastDanger._subtitle;
                    lblTitle.BackColor = _toastDanger._background;
                    lblSubtitle.BackColor = _toastDanger._background;
                    break;

                case toastClass.Warning:
                    pictureBox1.BackColor = _toastWarning._background;
                    this.BackColor = _toastWarning._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastWarning._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastWarning._title;
                    lblSubtitle.ForeColor = _toastWarning._subtitle;
                    lblTitle.BackColor = _toastWarning._background;
                    lblSubtitle.BackColor = _toastWarning._background;
                    break;

                case toastClass.Info:
                    pictureBox1.BackColor = _toastInfo._background;
                    this.BackColor = _toastInfo._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastInfo._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastInfo._title;
                    lblSubtitle.ForeColor = _toastInfo._subtitle;
                    lblTitle.BackColor = _toastInfo._background;
                    lblSubtitle.BackColor = _toastInfo._background;
                    break;

                case toastClass.Light:
                    pictureBox1.BackColor = _toastLight._background;
                    this.BackColor = _toastLight._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastLight._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastLight._title;
                    lblSubtitle.ForeColor = _toastLight._subtitle;
                    lblTitle.BackColor = _toastLight._background;
                    lblSubtitle.BackColor = _toastLight._background;
                    break;

                case toastClass.Dark:
                    pictureBox1.BackColor = _toastDark._background;
                    this.BackColor = _toastDark._border;
                    ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, _toastDark._border, ButtonBorderStyle.Solid);
                    lblTitle.ForeColor = _toastDark._title;
                    lblSubtitle.ForeColor = _toastDark._subtitle;
                    lblTitle.BackColor = _toastDark._background;
                    lblSubtitle.BackColor = _toastDark._background;
                    break;
            }
            
        }

        //para hacer el fade-out
        //si estoy clickeando sobre el form, tengo que esperar a que se deje de clickear para hacer el efecto
        //(sino se va a cerrar cuando estoy haciendole click encima)
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_clicked)
            {
                timer1.Enabled = false;
                do
                {
                    this.Opacity -= 0.05;
                    System.Threading.Thread.Sleep(10);
                } while (this.Opacity > 0);
                this.Close();
            }
        }

        //hace el efecto de "agrandar" el form cuando se presiona el mouse
        private void agrandar()
        {
            _clicked = true;
            this.Height += 4;
            this.Width += 4;
            this.Top -= 1;
            this.Left += 1;
            pictureBox1.Left = 2;
            pictureBox1.Top = 2;
        }

        //hace el efecto de "achicar" el form cuando se suelta el mouse
        private void achicar()
        {
            _clicked = false;
            this.Height -= 4;
            this.Width -= 4;
            this.Top += 1;
            this.Left -= 1;
            pictureBox1.Left = 1;
            pictureBox1.Top = 1;
        }

        //para cada control en el form, tengo que controlar 4 cosas:
        // MouseDown: cuando se presiona algun boton del mouse sobre la notif, hay que hacer el efecto de "agrandar" el form
        // MouseUp: cuando se suelta el boton del mouse, hay que hacer el efecto de "achicar" el form
        // MouseLeave: si el mouse salió del form cuando estaba presionado, hacer el efecto de "achicar" el form (*)
        // Click: cuando se completa la accion de click en el form, tengo que llamar al metodo que hace algo (**)

            // (*) como no hay manera de controlar que el evento del mouse (mousedown,mouseup,click) haya ocurrido
            // todo dentro del form hay que hacer un control especial:
            //si hice mousedown dentro del form, este se "agranda", pero si despues saco el mouse del area del form
            //y suelto el boton, el form NO recibe el evento mouseup (porque el mouse no está en el form con focus)
            //y por lo tanto, no se va a achicar al hacer mouseup.
            //entonces, al hacer mousedown uso la variable _clicked para marcar que empecé el evento de click.
            //cuando el mouse sale del area del form, si _clicked=true quiere decir que el form se "agrando" pero el mouse
            //se fue, entonces tengo que achicarlo cuando saco el mouse (sino va a quedar grande para siempre)

            // (**) el evento click se dispara cuando MouseDown y MouseUp ocurren uno detras del otro en el mismo form,
            // es decir, un Click es la accion completa de presionar y luego soltar un boton del mouse.

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            respuesta();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            agrandar();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            achicar();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            respuesta();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
         if(_clicked) { achicar(); }
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            agrandar();
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            achicar();
        }

        private void lblTitle_MouseLeave(object sender, EventArgs e)
        {
            if (_clicked) { achicar(); }
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {
            respuesta();
        }

        private void lblSubtitle_MouseDown(object sender, MouseEventArgs e)
        {
            agrandar();
        }

        private void lblSubtitle_MouseUp(object sender, MouseEventArgs e)
        {
            achicar();
        }

        private void lblSubtitle_MouseLeave(object sender, EventArgs e)
        {
            if (_clicked) { achicar(); }
        }
    }
}
