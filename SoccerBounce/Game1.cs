using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Collections;
using System;

namespace SoccerBounce
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balon;
        Texture2D porteroRojo;
        Texture2D porteroAzul;
        Texture2D background;
        Texture2D background2;
        Texture2D ghostPU;
        Texture2D miniPU;
        Texture2D giantPU;
        Texture2D largerPU;
        Texture2D player;
        Texture2D tachon;
        Texture2D cono;
        Texture2D imag;
        Texture2D vacia;
        Texture2D[] listaConos;
        Texture2D[] listaTachones;
        KeyboardState teclado;
        MouseState mouse, oldMouseState;
        SpriteFont letrero, titulo, botones, score, timer;
        List<ObjetosADibujar> listaObjsBuenos = new List<ObjetosADibujar>();
        ArrayList listaObjsMalos = new ArrayList();        
        ObjetosADibujar elemento;
        int xPosPlayer, yPosPlayer, pX, pY, vX, vY, selectImg, time, buenos, malos;
        int estadoJuego, estadoJuegoAnt, actual, objActual;
        const int numObjs = 8;
        bool val, marca;
        Rectangle bal, ghPwrU, mPwrU, gPwrU, lPwrU, boton1, boton2, boton3, mouseObj; //rectangulo con dimensiones de objetos para detectar colisiones
        SoundEffect goal, start, finish;
        Color ballColor;
        int xBall, yBall, xPR, yPR, xPA, yPA, deltaX, deltaY, sizeX, sizeY, xPU, yPU, valX, valY, alturaPA, alturaPR;
        int gol1, gol2, mins, segs, fms, fms2, fms3, fms4, fms5, fms6, powerUp, jugada;
        bool appearPU;
        Random rnd = new Random();
        Color menuBackground = new Color(0, 0, 0, 180), buttonColor = new Color(Color.Red, 190);
        Texture2D menuRect, buttonG1, buttonG2, buttonExit;
        int fms7 = 0, fms8 = 0;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            estadoJuego = 0;
            listaConos = new Texture2D[numObjs];
            actual = 0;
            listaTachones = new Texture2D[numObjs];
            objActual = 0;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // Usados en Menú Principal
            menuRect = CreateRectangle(600, 360, menuBackground);
            buttonG1 = CreateRectangle(180, 60, buttonColor);
            buttonG2 = CreateRectangle(180, 60, buttonColor);
            buttonExit = CreateRectangle(180, 60, buttonColor);
            titulo = Content.Load<SpriteFont>("Title");
            botones = Content.Load<SpriteFont>("buttons");

            //Usados en SoccerPong
            balon = Content.Load<Texture2D>("balon");
            porteroAzul = Content.Load<Texture2D>("porteroAzul");
            porteroRojo = Content.Load<Texture2D>("porteroRojo");
            background = Content.Load<Texture2D>("cancha2");
            ghostPU = Content.Load<Texture2D>("ghostPU");
            miniPU = Content.Load<Texture2D>("miniPU");
            giantPU = Content.Load<Texture2D>("giantPU");
            largerPU = Content.Load<Texture2D>("largerPU");

            score = Content.Load<SpriteFont>("marcador");
            timer = Content.Load<SpriteFont>("candara");

            goal = Content.Load<SoundEffect>("celebracion1");
            start = Content.Load<SoundEffect>("silbato");
            finish = Content.Load<SoundEffect>("silbato2");

            // Usados en SoccerFall
            background2 = Content.Load<Texture2D>("cancha");
            player = Content.Load<Texture2D>("jugador");
            letrero = Content.Load<SpriteFont>("Letrero");
            cono = Content.Load<Texture2D>("cono");
            tachon = Content.Load<Texture2D>("tachon");
            listaConos[0] = Content.Load<Texture2D>("cono");
            listaConos[1] = Content.Load<Texture2D>("cono2");
            listaConos[2] = Content.Load<Texture2D>("cono3");
            listaConos[3] = Content.Load<Texture2D>("cono4");
            listaConos[4] = Content.Load<Texture2D>("cono5");
            listaConos[5] = Content.Load<Texture2D>("cono6");
            listaConos[6] = Content.Load<Texture2D>("cono7");
            listaConos[7] = Content.Load<Texture2D>("cono8");
            listaTachones[0] = Content.Load<Texture2D>("tachon");
            listaTachones[1] = Content.Load<Texture2D>("tachon2");
            listaTachones[2] = Content.Load<Texture2D>("tachon3");
            listaTachones[3] = Content.Load<Texture2D>("tachon4");
            listaTachones[4] = Content.Load<Texture2D>("tachon5");
            listaTachones[5] = Content.Load<Texture2D>("tachon6");
            listaTachones[6] = Content.Load<Texture2D>("tachon7");
            listaTachones[7] = Content.Load<Texture2D>("tachon8");
            vacia = Content.Load<Texture2D>("vacia");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.P))
            {
                if (estadoJuego != 3)
                    estadoJuegoAnt = estadoJuego;
                if (estadoJuego != 0)
                    estadoJuego = 3;
            }
            
            switch (estadoJuego)
            {
                case 0:
                    updMenu();
                    this.IsMouseVisible = true;
                    break;         
                case 1:
                    updFirstGame();
                    this.IsMouseVisible = false;
                    break;
                case 2:
                    updSecondGame();
                    this.IsMouseVisible = false;
                    break;
                case 3:
                    updPause();
                    this.IsMouseVisible = true;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);
            spriteBatch.Begin();

            switch (estadoJuego)
            {
                case 0:
                    drawMenu();
                    break;
                case 1:
                    drawFirstGame();
                    break;
                case 2:
                    drawSecondGame();
                    break;
                case 3:
                    drawPause();
                    break;
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }

        void updMenu()
        {
            mouse = Mouse.GetState();

            boton1 = new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height);
            boton2 = new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height);
            boton3 = new Rectangle(400 - buttonExit.Width / 2, 330, buttonExit.Width, buttonExit.Height);
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed)
            {
                initFirstGame();                
                estadoJuego = 1;
            }
            if (mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed)
            {
                initSecondGame();
                estadoJuego = 2;
            }
            if (estadoJuegoAnt == 3)
            {
                fms8++;
                if (mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed && fms8 >= 10)
                {               
                    this.Exit();
                    fms8 = 0;
                }
            }
        }
    
        void drawMenu()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height), Color.White);
            spriteBatch.Draw(buttonG2, new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height), Color.White);
            spriteBatch.Draw(buttonExit, new Rectangle(400 - buttonExit.Width / 2, 330, buttonExit.Width, buttonExit.Height), Color.White);
            spriteBatch.DrawString(titulo, "SoccerBounce", new Vector2(225, 70), Color.Blue);
            spriteBatch.DrawString(botones, "SoccerPong", new Vector2(335, 155), Color.Black);
            spriteBatch.DrawString(botones, "SoccerFall", new Vector2(340, 250), Color.Black);
            spriteBatch.DrawString(botones, "Exit", new Vector2(375, 345), Color.Black);         
        }

        void updPause()
        {
            mouse = Mouse.GetState();
            boton1 = new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height);
            boton2 = new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height);
            boton3 = new Rectangle(400 - buttonExit.Width / 2, 330, buttonExit.Width, buttonExit.Height);
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed)
            {
                if (estadoJuegoAnt == 1)
                    estadoJuego = 1;
                else if (estadoJuegoAnt == 2)
                    estadoJuego = 2;
            }
            if (mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed)
            {
                if (estadoJuegoAnt == 1)
                {
                    initFirstGame();
                    estadoJuego = 1;
                }else if (estadoJuegoAnt == 2)
                {
                    initSecondGame();
                    estadoJuego = 2;                   
                }
            }
            if (mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed)
            {
                fms8 = 0;
                estadoJuegoAnt = estadoJuego;
                estadoJuego = 0;
            }
        }

        void drawPause()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height), Color.White);
            spriteBatch.Draw(buttonG2, new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height), Color.White);
            spriteBatch.Draw(buttonExit, new Rectangle(400 - buttonExit.Width / 2, 330, buttonExit.Width, buttonExit.Height), Color.White);
            spriteBatch.DrawString(botones, "Continue", new Vector2(355, 155), Color.Black);
            spriteBatch.DrawString(botones, "Restart", new Vector2(360, 250), Color.Black);
            spriteBatch.DrawString(botones, "Main Menu", new Vector2(340, 345), Color.Black);
        }

        void initFirstGame()
        {
            xBall = graphics.GraphicsDevice.Viewport.Width / 2 - 20;
            yBall = graphics.GraphicsDevice.Viewport.Height / 2 - 20;
            xPR = 105;
            yPR = graphics.GraphicsDevice.Viewport.Height / 2 - 22;
            xPA = 670;
            yPA = graphics.GraphicsDevice.Viewport.Height / 2 - 22;

            do
            {
                deltaX = rnd.Next(-7, 8);
            } while (deltaX > -5 && deltaX < 5);

            do
            {
                deltaY = rnd.Next(-7, 8);
            } while (deltaY > -5 && deltaY < 5);

            ballColor = Color.White;
            fms3 = 610;
            fms4 = 610;
            fms5 = 610;
            fms = 0;
            yPU = -70;
            appearPU = false;
            alturaPA = 72;
            alturaPR = 72;
            jugada = 0;
            mins = 0;
            segs = 0;
            gol1 = 0;
            gol2 = 0;
        }

        void updFirstGame()
        {
            if (xBall > (graphics.GraphicsDevice.Viewport.Width - sizeX) || xBall < 0)
            {
                deltaX *= -1;
            }

            if (yBall > (graphics.GraphicsDevice.Viewport.Height - sizeY) || yBall < 0)
                deltaY *= -1;

            if (xBall < 0 && (yBall > 150 && yBall < 250))
            {
                gol2++;
                deltaX = rnd.Next(5, 8);
                deltaY = rnd.Next(5, 8);
                goal.Play();
            }

            if (xBall > (graphics.GraphicsDevice.Viewport.Width - sizeX) && (yBall > 150 && yBall < 250))
            {
                gol1++;
                deltaY = rnd.Next(5, 8);
                deltaX = rnd.Next(5, 8) * -1;
                goal.Play();
            }

            if (xBall > (graphics.GraphicsDevice.Viewport.Width - sizeX))
                xBall = graphics.GraphicsDevice.Viewport.Width - sizeX;
            if (xBall < 0)
                xBall = 0;
            if (yBall > graphics.GraphicsDevice.Viewport.Height - sizeY)
                yBall = graphics.GraphicsDevice.Viewport.Height - sizeY;
            if (yBall < 0)
                yBall = 0;

            xBall += deltaX;
            yBall += deltaY;

            fms++;

            if (fms == 60)
            {
                segs++;
                fms = 0;
            }

            if (segs == 60)
            {
                mins++;
                segs = 0;
            }

            if (mins == 0 && segs == 0)
                start.Play();

            if (mins == 1 && segs == 30 && fms < 60)
            {
                xBall = graphics.GraphicsDevice.Viewport.Width / 2 - sizeX / 2;
                yBall = graphics.GraphicsDevice.Viewport.Height / 2 - sizeY / 2;
                deltaX = 0;
                deltaY = 0;
                fms = 65;
                finish.Play();
                estadoJuego = 0;
                initFirstGame();
            }

            fms2++;
            fms3++;
            fms4++;
            fms5++;
            fms6++;

            teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.W))
                yPR -= 3;
            if (teclado.IsKeyDown(Keys.S))
                yPR += 3;
            if (teclado.IsKeyDown(Keys.Up))
                yPA -= 3;
            if (teclado.IsKeyDown(Keys.Down))
                yPA += 3;


            if (fms4 < 610 && powerUp == 3)
            {
                sizeX = 95;
                sizeY = 95;
                appearPU = false;
            }
            else if (fms4 > 610 && fms5 > 610)
            {
                sizeX = balon.Width;
                sizeY = balon.Height;
            }

            if (fms5 < 610 && powerUp == 2)
            {
                sizeX = 15;
                sizeY = 15;
                appearPU = false;
            }
            else if (fms5 > 610 && fms4 > 610)
            {
                sizeX = balon.Width;
                sizeY = balon.Height;
            }

            if (fms3 < 610 && powerUp == 1)
            {
                if (fms2 == 10)
                    ballColor = Color.Transparent;
                if (fms2 == 20)
                {
                    ballColor = Color.White;
                    fms2 = 0;
                }
                appearPU = false;
            }

            if (fms6 < 610 && powerUp == 4)
            {
                if (jugada == 1 && alturaPA < 135)
                    alturaPR = 135;
                else if (jugada == 2 && alturaPR < 135)
                    alturaPA = 135;
                appearPU = false;
            }
            else if (fms6 > 610)
            {
                alturaPA = porteroAzul.Height;
                alturaPR = porteroRojo.Height;
            }

            bal = new Rectangle(xBall, yBall, sizeX, sizeY);

            if (bal.Intersects(new Rectangle(xPA, yPA + alturaPA / 5, porteroAzul.Width / 2, alturaPA / 2)))
            {
                if (xBall > xPA)
                    deltaX = rnd.Next(5, 8);
                else
                    deltaX = rnd.Next(5, 8) * -1;
                jugada = 2;
            }

            if (bal.Intersects(new Rectangle(xPR, yPR + alturaPR / 5, porteroRojo.Width / 2, alturaPR / 2)))
            {
                if (xBall < xPR)
                    deltaX = rnd.Next(5, 8) * -1;
                else
                    deltaX = rnd.Next(5, 8);
                jugada = 1;
            }


            if (segs % 20 == 0 && (segs != 0 || mins != 0) && appearPU == false)
            {
                powerUp = rnd.Next(1, 6);
                appearPU = true;
                xPU = rnd.Next(70, 710);
                yPU = rnd.Next(70, 410);
            }

            ghPwrU = new Rectangle(xPU, yPU, 30, 30);
            mPwrU = new Rectangle(xPU, yPU, 30, 30);
            gPwrU = new Rectangle(xPU, yPU, 30, 30);
            lPwrU = new Rectangle(xPU, yPU, 30, 30);

            if (bal.Intersects(lPwrU) && jugada != 0)
            {
                fms6 = 0;
                yPU = -70;
                appearPU = false;
            }

            if (bal.Intersects(ghPwrU) && powerUp == 1)
            {
                fms2 = 0;
                fms3 = 0;
                yPU = -70;
                appearPU = false;
            }

            if (bal.Intersects(mPwrU) && powerUp == 2)
            {
                fms5 = 0;
                yPU = -70;
                appearPU = false;
            }

            if (bal.Intersects(gPwrU) && powerUp == 3)
            {
                fms4 = 0;
                yPU = -70;
                appearPU = false;
            }
            
            if (yPR + alturaPR / 3 < 148)
                yPR = 148 - alturaPR / 3;
            if (yPR - alturaPR / 3 > 270)
                yPR = 270 + alturaPR / 3;          //Limita el movimiento de los porteros al área grande
            if (yPA + alturaPA / 3 < 148)
                yPA = 148 - alturaPA / 3;
            if (yPA - alturaPA / 3 > 270)
                yPA = 270 + alturaPA / 3;
        }

        void drawFirstGame()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(porteroAzul, new Rectangle(xPA, yPA, porteroAzul.Width, alturaPA), Color.White);
            spriteBatch.Draw(porteroRojo, new Rectangle(xPR, yPR, porteroRojo.Width, alturaPR), Color.White);
            spriteBatch.Draw(balon, new Rectangle(xBall, yBall, sizeX, sizeY), ballColor);            
           // spriteBatch.Draw(largerPU, new Rectangle(200, -70, largerPU.Width, largerPU.Height), Color.White);

            switch (powerUp)
            {
                case 1:
                    spriteBatch.Draw(ghostPU, new Rectangle(xPU, yPU, ghostPU.Width, ghostPU.Height), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(miniPU, new Rectangle(xPU, yPU, miniPU.Width, miniPU.Height), Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(giantPU, new Rectangle(xPU, yPU, giantPU.Width, giantPU.Height), Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(largerPU, new Rectangle(xPU, yPU, largerPU.Width, largerPU.Height), Color.White);
                    break;
                case 5:
                    yPU = -70;
                    break;
            }

            spriteBatch.DrawString(score, gol1.ToString(), new Vector2(340, 18), Color.Red);
            spriteBatch.DrawString(score, gol2.ToString(), new Vector2(442, 18), Color.DarkBlue);
            spriteBatch.DrawString(timer, mins.ToString("00") + ":" + segs.ToString("00"), new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 23), Color.White);
        }

        void initSecondGame()
        {
            xPosPlayer = graphics.GraphicsDevice.Viewport.Width / 2 - 20;
            yPosPlayer = 440;

            selectImg = rnd.Next(1, 4);
            vX = 0;
            vY = rnd.Next(2, 4);
            pX = rnd.Next(50, 700);
            pY = rnd.Next(250) * -1;
            buenos = 0;
            malos = 0;
            marca = false;
            listaObjsBuenos.Clear();
            listaObjsMalos.Clear();
        }

        void updSecondGame()
        {
            teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.Left))
                xPosPlayer -= 4;
            if (teclado.IsKeyDown(Keys.Right))
                xPosPlayer += 4;
            if (teclado.IsKeyDown(Keys.Up))
                yPosPlayer -= 4;
            if (teclado.IsKeyDown(Keys.Down))
                yPosPlayer += 4;

            if (xPosPlayer < 0)
                xPosPlayer = 0;
            if (xPosPlayer > graphics.GraphicsDevice.Viewport.Width - player.Width)
                xPosPlayer = graphics.GraphicsDevice.Viewport.Width - player.Width;
            if (yPosPlayer > 440)
                yPosPlayer = 440;
            if (yPosPlayer < 355)
                yPosPlayer = 355;

            switch (selectImg)
            {
                case 1:
                    if (time % 35 == 0)
                    {
                        elemento = new ObjetosADibujar(pX, pY, vX, vY, balon, true, true);
                        listaObjsBuenos.Add(elemento);
                    }
                    break;
                case 2:
                    if (time % 25 == 0)
                    {
                        elemento = new ObjetosADibujar(pX, pY, vX, vY, listaTachones, false, true, objActual);
                        listaObjsMalos.Add(elemento);
                        time = 0;
                    }
                    break;
                case 3:
                    if (time % 25 == 0)
                    {
                        elemento = new ObjetosADibujar(pX, pY, vX, vY, listaConos, true, true, objActual);
                        listaObjsMalos.Add(elemento);
                        time = 0;
                    }
                    break;
            }           

         /*   fms7++;
            if (fms7 == 12)
            {
                objActual++;
                fms7 = 0;
            }
           
            if (objActual >= numObjs)
                objActual = 0;
                */
            time++;

            Rectangle areaJugador = new Rectangle(xPosPlayer, yPosPlayer, player.Width, player.Height);

            foreach (ObjetosADibujar obj in listaObjsBuenos)
            {
                obj.PosicY += obj.VelY;
                obj.PosicX += obj.VelX;
                
                Rectangle areaObjetoBueno = new Rectangle(obj.PosicX, obj.PosicY, obj.Imagen.Width, obj.Imagen.Height);
                                           
                if (areaJugador.Intersects(areaObjetoBueno))
                {
                    obj.VelY *= -1;
                    obj.VelX = rnd.Next(-2, 3);
                    buenos++;
                }
                                              
                if (obj.PosicY > graphics.GraphicsDevice.Viewport.Height || obj.PosicY < -250)
                    obj.Marca = false;
                if (obj.PosicY + obj.Imagen.Height >= yPosPlayer && obj.VelY < 0)
                    obj.PosicY = yPosPlayer - obj.Imagen.Height;
            }

            foreach (ObjetosADibujar obj in listaObjsMalos)
            {
                obj.PosicY += obj.VelY;
                obj.PosicX += obj.VelX;

                Rectangle areaObjetoMalo = new Rectangle(obj.PosicX, obj.PosicY, obj.Imagenes[0].Width, obj.Imagenes[0].Height);
               
                if (areaJugador.Intersects(areaObjetoMalo))
                {
                    malos++;
                    obj.PosicY = 500;
                }

                if (obj.PosicY > graphics.GraphicsDevice.Viewport.Height || obj.PosicY < -250)
                    obj.Marca = false;
                if (obj.PosicY + obj.Imagenes[0].Height >= yPosPlayer && obj.VelY < 0)
                    obj.PosicY = yPosPlayer - obj.Imagenes[0].Height;
            }

            foreach (ObjetosADibujar objBn in listaObjsBuenos)
            {
                Rectangle areaObjetoBueno = new Rectangle(objBn.PosicX, objBn.PosicY, objBn.Imagen.Width, objBn.Imagen.Height);
                foreach (ObjetosADibujar objMl in listaObjsMalos)
                {
                    Rectangle areaObjetoMalo = new Rectangle(objMl.PosicX, objMl.PosicY, objMl.Imagenes[0].Width, objMl.Imagenes[0].Height);
                    if (objBn.VelY < 0)
                    {
                        if (areaObjetoBueno.Intersects(areaObjetoMalo))
                            objMl.PosicY = 500;
                    }
                }
            }

            for (int i = listaObjsBuenos.Count - 1; i >= 0; i--)
            {
                if (listaObjsBuenos[i].Marca == false)
                    listaObjsBuenos.Remove(listaObjsBuenos[i]);
            }

            ObjetosADibujar[] arrayObjsMalos = listaObjsMalos.ToArray(typeof(ObjetosADibujar)) as ObjetosADibujar[];

            for (int i = listaObjsMalos.Count - 1; i >= 0; i--)
            {
                if (arrayObjsMalos[i].Marca == false)
                    listaObjsMalos.Remove(listaObjsMalos[i]);
            }
            
            selectImg = rnd.Next(1, 4);
            vY = rnd.Next(1, 4);
            pX = rnd.Next(100, 700);
            pY = rnd.Next(250) * -1;
        }

        void drawSecondGame()
        {
            spriteBatch.Draw(background2, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(player, new Rectangle(xPosPlayer, yPosPlayer, player.Width, player.Height), Color.White);
            spriteBatch.DrawString(letrero, "Buenos " + buenos, new Vector2(20, 25), Color.White);
            spriteBatch.DrawString(letrero, "Malos " + malos, new Vector2(685, 25), Color.White);

            foreach (ObjetosADibujar obj in listaObjsBuenos) {
                spriteBatch.Draw(obj.Imagen, new Rectangle(obj.PosicX, obj.PosicY, obj.Imagen.Width, obj.Imagen.Height), Color.White);                
            }
            fms7++;
            if (fms7 == 8)
            {
                 actual++;
                fms7 = 0;
                if (actual >= 8)
                    actual = 0;
            }
            foreach (ObjetosADibujar obj in listaObjsMalos)
            {
                obj.Actual = actual;
                spriteBatch.Draw(obj.Imagenes[obj.Actual], new Rectangle(obj.PosicX, obj.PosicY, obj.Imagenes[0].Width, obj.Imagenes[0].Height), Color.White);
                
            }
        }
        
        private Texture2D CreateRectangle(int width, int height, Color col)
        {
           Texture2D texture = new Texture2D(graphics.GraphicsDevice, width, height, false, SurfaceFormat.Color);
            Color[] color = new Color[width * height];

            for (int i = 0; i < color.Length; i++)
                color[i] = col;
            texture.SetData(color);
            return texture;
        }
    }


    internal class ObjetosADibujar
    {
        private int posicX;
        private int posicY;
        private int velX;
        private int velY;
        private int actual;
        private Texture2D imagen;
        private Texture2D[] imagenes;
        private bool valoracion; //True-bueno, False-malo
        private bool marca; //True-activo,  False-inactivo

        public ObjetosADibujar()
        {
        }

        public ObjetosADibujar(int pX, int pY, int vX, int vY, Texture2D img, bool val, bool mrc)
        {            
            posicX = pX;
            posicY = pY;
            velX = vX;
            velY = vY;
            imagen = img;
            valoracion = val;
            marca = mrc;
        }

        public ObjetosADibujar(int pX, int pY, int vX, int vY, Texture2D[] img, bool val, bool mrc, int act)
        {
            posicX = pX;
            posicY = pY;
            velX = vX;
            velY = vY;
            imagenes = img;
            valoracion = val;
            marca = mrc;
            actual = act;
        }

        public int PosicX
        {
            get { return posicX; }
            set { posicX = value; }
        }

        public int PosicY
        {
            get { return posicY; }
            set { posicY = value; }
        }

        public int VelX
        {
            get { return velX; }
            set { velX = value; }
        }

        public int VelY
        {
            get { return velY; }
            set { velY = value; }
        }

        public Texture2D Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        public Texture2D[] Imagenes
        {
            get { return imagenes; }
            set { imagenes = value; }
        }

        public bool Valoracion
        {
            get { return valoracion; }
            set { valoracion = value; }
        }

        public bool Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public int Actual
        {
            get { return actual; }
            set { actual = value; }
        }
    }
}
