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
        Texture2D balon, balonChico, porteroRojo, porteroAzul, background, background2, ghostPU, miniPU, giantPU, largerPU;
        Texture2D imag, vacia, player, tachon, cono, instruccionesSPong, instruccionesSFall, instruccionesSTable, instruccionPausa;
        Texture2D[] equipoRojo, equipoAzul, listaConos, listaTachones;
        KeyboardState teclado;
        MouseState mouse, oldMouseState;
        SpriteFont letrero, titulo, botones, score, timer, relojEspera, marcadorFinal, marcadorFinal2, resultados;
        List<ObjetosADibujar> listaObjsBuenos = new List<ObjetosADibujar>();
        ArrayList listaObjsMalos = new ArrayList();
        ObjetosADibujar elemento;
        int xPosPlayer, yPosPlayer, pX, pY, vX, vY, selectImg, time, salvados, vidas, recibidos;
        int estadoJuego, estadoJuegoAnt = -1, actual, objActual, rojoActual, azulActual, tiempoEsperaGol, esperaGol, kickDelayAzul, kickDelayRojo, menuDelay;
        const int numObjs = 8;
        bool val, marca, reducir, patadaRoja, patadaAzul, appearPU, golAzul, golRojo, patadaAz, patadaRj, choseKey, juegoXtiempo, juegoXgoles;
        Rectangle bal, ghPwrU, mPwrU, gPwrU, lPwrU, boton1, boton2, boton3, boton4, mouseObj; //rectangulo con dimensiones de objetos para detectar colisiones
        Rectangle rojo1, rojo2, rojo3, rojo4, rojo5, rojo6, rojo7, rojo8, rojo9, azul1, azul2, azul3, azul4, azul5, azul6, azul7, azul8, azul9;
        Rectangle botonUnJugador, botonDosJugadores, botonUnMinuto, botonTresMinutos, botonTiempo, botonGoles, botonCincoGoles, botonSieteGoles, botonDiezGoles;
        SoundEffect goal, start, finish, smack;
        Color ballColor, colorGanador;
        int xBall, yBall, xPR, yPR, xPA, yPA, xEA, yEA, xER, yER, deltaX, deltaY, sizeX, sizeY, xPU, yPU, valX, valY, alturaPA, alturaPR;
        int golesRojo, golesAzul, mins, segs, reloj, fms2, fms3, fms4, fms5, fms6, tiempoPatadaRoja, tiempoPatadaAzul, powerUp, jugada, numJugadores, tiempoPartido, numGoles, opcionesEscogidas;
        Random rnd = new Random(), rnd2 = new Random();
        Color menuBackground = new Color(0, 0, 0, 180), buttonColor = new Color(Color.Red, 190), buttonG1Color, buttonG2Color, buttonG3Color, buttonExitColor, buttonInstructionsColor;
        Texture2D menuRect, buttonG1, buttonG2, buttonG3, buttonExit, botonVacio, buttonInstructions;
        Texture2D jugador;
        int yRojo1, yRojo2, yRojo3, yRojo4, yRojo5, yRojo6, yRojo7, yRojo8, yRojo9;
        int yAzul1, yAzul2, yAzul3, yAzul4, yAzul5, yAzul6, yAzul7, yAzul8, yAzul9;
        int fms7 = 0, fms8 = 0;
        string ganador, game;
        

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
            equipoAzul = new Texture2D[2];
            azulActual = 0;
            equipoRojo = new Texture2D[2];
            rojoActual = 0;
            buttonG1Color = Color.White;
            buttonG2Color = Color.White;
            buttonG3Color = Color.White;
            buttonExitColor = Color.White;
            buttonInstructionsColor = Color.White;

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
           
            // Usados en Menú 
            menuRect = CreateRectangle(600, 360, menuBackground);
            buttonG1 = CreateRectangle(180, 60, buttonColor);
            buttonG2 = CreateRectangle(180, 60, buttonColor);
            buttonG3 = CreateRectangle(180, 60, buttonColor);
            buttonInstructions = CreateRectangle(155, 40, buttonColor);
            buttonExit = CreateRectangle(80, 40, buttonColor);
            botonVacio = Content.Load<Texture2D>("botonVacio2");            
            titulo = Content.Load<SpriteFont>("Title");
            botones = Content.Load<SpriteFont>("buttons");
            instruccionPausa = Content.Load<Texture2D>("pausa");
            
            //Usados en SoccerPong
            balon = Content.Load<Texture2D>("balon");
            porteroAzul = Content.Load<Texture2D>("porteroAzul");
            porteroRojo = Content.Load<Texture2D>("porteroRojo");
            background = Content.Load<Texture2D>("cancha2");
            ghostPU = Content.Load<Texture2D>("ghostPU");
            miniPU = Content.Load<Texture2D>("miniPU");
            giantPU = Content.Load<Texture2D>("giantPU");
            largerPU = Content.Load<Texture2D>("largerPU");
            instruccionesSPong = Content.Load<Texture2D>("InstruccionesSPong");
            score = Content.Load<SpriteFont>("marcador");
            timer = Content.Load<SpriteFont>("candara");
            marcadorFinal = Content.Load<SpriteFont>("marcadorFinal");
            marcadorFinal2 = Content.Load<SpriteFont>("marcadorFinal2");
            goal = Content.Load<SoundEffect>("celebracion1");
            start = Content.Load<SoundEffect>("silbato");
            finish = Content.Load<SoundEffect>("silbato2");

            // Usados en SoccerFall
            background2 = Content.Load<Texture2D>("cancha");
            player = Content.Load<Texture2D>("jugador");
            letrero = Content.Load<SpriteFont>("Letrero");
            resultados = Content.Load<SpriteFont>("Resultados");
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
            instruccionesSFall = Content.Load<Texture2D>("InstruccionesSFall");
            vacia = Content.Load<Texture2D>("vacia");
            smack = Content.Load<SoundEffect>("smack");

            //Usados en SoccerTable
            equipoAzul[0] = Content.Load<Texture2D>("EquipoAzul");
            equipoAzul[1] = Content.Load<Texture2D>("EquipoAzulPt");
            equipoRojo[0] = Content.Load<Texture2D>("EquipoRojo");
            equipoRojo[1] = Content.Load<Texture2D>("EquipoRojoPt");
            balonChico = Content.Load<Texture2D>("balonChico");
            instruccionesSTable = Content.Load<Texture2D>("InstruccionesSTable");
            relojEspera = Content.Load<SpriteFont>("relojEspera");
            jugador = CreateRectangle(20, 35, Color.YellowGreen);
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
                    updSoccerPong();
                    this.IsMouseVisible = false;
                    break;
                case 2:
                    updSoccerFall();
                    this.IsMouseVisible = false;
                    break;                
                case 3:
                    updPause();
                    this.IsMouseVisible = true;
                    break;
                case 4:
                    updSoccerTable();
                    this.IsMouseVisible = false;
                    break;
                case 5:
                    updGameMenu();
                    this.IsMouseVisible = true;
                    break;
                case 7:
                    updEndOfGame();
                    this.IsMouseVisible = true;
                    break;
                case 8:
                    updInstructions();
                    this.IsMouseVisible = true;
                    break;
                case 9:
                    updEndOfGame2();
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(background2, new Rectangle(0, 0, 800, 480), Color.White);
            switch (estadoJuego)
            {
                case 0:
                    drawMenu();
                    break;
                case 1:
                    drawSoccerPong();
                    break;
                case 2:
                    drawSoccerFall();
                    break;
                case 3:
                    drawPause();
                    break;
                case 4:
                    drawSoccerTable();
                    break;
                case 5:
                    drawGameMenu();
                    break;
                case 7:
                    drawEndOfGame();
                    break;
                case 8:
                    drawInstructions();
                    break;
                case 9:
                    drawEndOfGame2();
                    break;
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }

        void updMenu()
        {
            mouse = Mouse.GetState();
            teclado = Keyboard.GetState();

            boton1 = new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height);
            boton2 = new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height);
            boton3 = new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height);
            boton4 = new Rectangle(610, 370, buttonExit.Width, buttonExit.Height);
            
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);
          
            if ((mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed) || (buttonG1Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG1Color = Color.White;
                buttonG2Color = Color.White;
                buttonG3Color = Color.White;
                buttonExitColor = Color.White;
                estadoJuegoAnt = estadoJuego;      
                estadoJuego = 5;
                fms8 = 0;
                game = "SoccerPong";
            }
            if ((mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed) || (buttonG2Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG1Color = Color.White;
                buttonG2Color = Color.White;
                buttonG3Color = Color.White;
                buttonExitColor = Color.White;
                estadoJuego = 8;
                fms8 = 0;
                game = "SoccerFall";
            }         
            if (estadoJuegoAnt != 0)
            {
                fms8++;
                if ((mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed && fms8 >= 15) || (buttonG3Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
                {
                    buttonG1Color = Color.White;
                    buttonG2Color = Color.White;
                    buttonG3Color = Color.White;
                    buttonExitColor = Color.White;
                    estadoJuegoAnt = 0;
                    estadoJuego = 5;
                    fms8 = 0;
                    game = "SoccerTable";
                }
            } 
            if (estadoJuegoAnt == 0)
            {                
                if ((mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed) || (buttonG3Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
                {
                    estadoJuegoAnt = 0;
                    buttonG1Color = Color.White;
                    buttonG2Color = Color.White;
                    buttonG3Color = Color.White;
                    buttonExitColor = Color.White;
                    estadoJuego = 5;
                    fms8 = 0;
                    game = "SoccerTable";
                }
            }
            if (estadoJuegoAnt == 5)
            {
                fms8++;
                if ((mouseObj.Intersects(boton4) && mouse.LeftButton == ButtonState.Pressed && fms8 >= 10) || (buttonExitColor == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
                {
                    this.Exit();
                }
            }
            if (estadoJuegoAnt != 5)
            {
                if ((mouseObj.Intersects(boton4) && mouse.LeftButton == ButtonState.Pressed) || (buttonExitColor == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
                {
                    this.Exit();
                }
            }

            if (menuDelay == 0)
            {
                if (teclado.IsKeyDown(Keys.Down))
                {
                    choseKey = true;
                    if (buttonG1Color == Color.CadetBlue)
                    {
                        buttonG1Color = Color.White;
                        buttonG2Color = Color.CadetBlue;
                    }
                    else if (buttonG2Color == Color.CadetBlue)
                    {
                        buttonG2Color = Color.White;
                        buttonG3Color = Color.CadetBlue;
                    }
                    else if (buttonG3Color == Color.CadetBlue)
                    {
                        buttonG3Color = Color.White;
                        buttonExitColor = Color.CadetBlue;
                    }
                    else
                    {
                        buttonExitColor = Color.White;
                        buttonG1Color = Color.CadetBlue;
                    }
                }

                    if (teclado.IsKeyDown(Keys.Up))
                    {
                        choseKey = true;
                        if (buttonG1Color == Color.CadetBlue)
                        {
                            buttonG1Color = Color.White;
                            buttonExitColor = Color.CadetBlue;
                        }
                        else if (buttonG2Color == Color.CadetBlue)
                        {
                            buttonG2Color = Color.White;
                            buttonG1Color = Color.CadetBlue;
                        }
                        else if (buttonG3Color == Color.CadetBlue)
                        {
                            buttonG3Color = Color.White;
                            buttonG2Color = Color.CadetBlue;
                        }
                        else if (buttonExitColor == Color.CadetBlue)
                        {
                            buttonExitColor = Color.White;
                            buttonG3Color = Color.CadetBlue;
                        }
                        else
                            buttonExitColor = Color.CadetBlue;
                    }
            }

            if (choseKey == true)
                menuDelay++;
            if (menuDelay == 15)
            {
                menuDelay = 0;
                choseKey = false;
            }            
            
        }
    
        void drawMenu()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height), buttonG1Color);
            spriteBatch.Draw(buttonG2, new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height), buttonG2Color);
            spriteBatch.Draw(buttonG3, new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height), buttonG3Color);
            spriteBatch.Draw(buttonExit, new Rectangle(610, 370, buttonExit.Width, buttonExit.Height), buttonExitColor);
            spriteBatch.Draw(instruccionPausa, new Rectangle(125, 370, 170, 50), Color.White);
            spriteBatch.DrawString(titulo, "SoccerBounce", new Vector2(225, 70), Color.Blue);
            spriteBatch.DrawString(botones, "SoccerPong", new Vector2(335, 155), Color.Black);
            spriteBatch.DrawString(botones, "SoccerFall", new Vector2(340, 250), Color.Black);
            spriteBatch.DrawString(botones, "SoccerTable", new Vector2(332, 345), Color.Black);
            spriteBatch.DrawString(botones, "Exit", new Vector2(628, 377), Color.Black);
        }

        void updPause()
        {
            mouse = Mouse.GetState();
            teclado = Keyboard.GetState();
            boton1 = new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height);
            boton2 = new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height);
            boton3 = new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height);
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if ((mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed) || (buttonG1Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG1Color = Color.White;
                if (estadoJuegoAnt == 1)
                    estadoJuego = 1;
                else if (estadoJuegoAnt == 2)
                    estadoJuego = 2;
                else if (estadoJuegoAnt == 4)
                    estadoJuego = 4;
            }
            if ((mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed) || (buttonG2Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG2Color = Color.White;
                if (estadoJuegoAnt == 1)
                {
                    initSoccerPong();
                    estadoJuego = 1;
                }else if (estadoJuegoAnt == 2)
                {
                    initSoccerFall();
                    estadoJuego = 2;                   
                } else if (estadoJuegoAnt == 4)
                {
                    initSoccerTable();
                    estadoJuego = 4;
                }
            }
            if ((mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed) || (buttonG3Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG3Color = Color.White;
                fms8 = 0;
                estadoJuegoAnt = estadoJuego;
                estadoJuego = 0;                
            }

            if (menuDelay == 0)
            {
                if (teclado.IsKeyDown(Keys.Down))
                {
                    choseKey = true;
                    if (buttonG1Color == Color.CadetBlue)
                    {
                        buttonG1Color = Color.White;
                        buttonG2Color = Color.CadetBlue;
                    }
                    else if (buttonG2Color == Color.CadetBlue)
                    {
                        buttonG2Color = Color.White;
                        buttonG3Color = Color.CadetBlue;
                    }
                    else if (buttonG3Color == Color.CadetBlue)
                    {
                        buttonG3Color = Color.White;
                        buttonG1Color = Color.CadetBlue;
                    }
                    else
                        buttonG1Color = Color.CadetBlue;                    
                }

                if (teclado.IsKeyDown(Keys.Up))
                {
                    choseKey = true;
                    if (buttonG1Color == Color.CadetBlue)
                    {
                        buttonG1Color = Color.White;
                        buttonG3Color = Color.CadetBlue;
                    }
                    else if (buttonG2Color == Color.CadetBlue)
                    {
                        buttonG2Color = Color.White;
                        buttonG1Color = Color.CadetBlue;
                    }
                    else if (buttonG3Color == Color.CadetBlue)
                    {
                        buttonG3Color = Color.White;
                        buttonG2Color = Color.CadetBlue;
                    }                    
                    else
                        buttonG3Color = Color.CadetBlue;
                }
            }

            if (choseKey == true)
                menuDelay++;
            if (menuDelay == 15)
            {
                menuDelay = 0;
                choseKey = false;
            }

        }

        void drawPause()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height), buttonG1Color);
            spriteBatch.Draw(buttonG2, new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height), buttonG2Color);
            spriteBatch.Draw(buttonG3, new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height), buttonG3Color);
            spriteBatch.DrawString(titulo, "Pause", new Vector2(326, 70), Color.Blue);
            spriteBatch.DrawString(botones, "Continue", new Vector2(355, 155), Color.Black);
            spriteBatch.DrawString(botones, "Restart", new Vector2(360, 250), Color.Black);
            spriteBatch.DrawString(botones, "Main Menu", new Vector2(340, 345), Color.Black);
        }

        void initSoccerPong()
        {
            start.Play();
            xBall = graphics.GraphicsDevice.Viewport.Width / 2 - 20;
            yBall = graphics.GraphicsDevice.Viewport.Height / 2 - 20;
            xPR = 105;
            yPR = graphics.GraphicsDevice.Viewport.Height / 2 - 22;
            xPA = 670;
            yPA = graphics.GraphicsDevice.Viewport.Height / 2 - 22;

            do
            {
                deltaX = rnd.Next(-7, 8);
            } while (deltaX > -3 && deltaX < 3);

            do
            {
                deltaY = rnd.Next(-7, 8);
            } while (deltaY > -5 && deltaY < 5);

            ballColor = Color.White;
            fms3 = 610;
            fms4 = 610;
            fms5 = 610;
            reloj = 0;
            yPU = -70;
            appearPU = false;
            alturaPA = 72;
            alturaPR = 72;
            jugada = 0;
            if (juegoXtiempo == true)
            {
                if (tiempoPartido == 1)
                {
                    mins = 1;
                    segs = 30;
                }else if (tiempoPartido == 3)
                {
                    mins = 3;
                    segs = 0;
                }
            }else if (juegoXgoles == true)
            {
                mins = 0;
                segs = 0;
            }
            
            golesRojo = 0;
            golesAzul = 0;
        }

        void updSoccerPong()
        {
            if (xBall > (graphics.GraphicsDevice.Viewport.Width - sizeX) || xBall < 0)
            {
                deltaX *= -1;
            }

            if (yBall > (graphics.GraphicsDevice.Viewport.Height - sizeY) || yBall < 0)
                deltaY *= -1;

            if (xBall < 0 && (yBall > 150 && yBall < 250))
            {
                golesAzul++;
                deltaX = rnd.Next(6, 8);
                deltaY = rnd.Next(5, 7);
                goal.Play();
            }

            if (xBall > (graphics.GraphicsDevice.Viewport.Width - sizeX) && (yBall > 150 && yBall < 250))
            {
                golesRojo++;
                deltaY = rnd.Next(5, 7);
                deltaX = rnd.Next(6, 8) * -1;
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

            reloj++;

            if (reloj == 60)
            {
                if (juegoXgoles == true)
                    segs++;
                else if (juegoXtiempo == true)
                    segs--;
                reloj = 0;
            }

            if (segs < 0)
            {
                mins--;
                segs = 59;
            }
            if (segs > 59)
            {
                mins++;
                segs = 0;
            }

            if (juegoXtiempo == true)
            {
                if (mins == 0 && segs == 0)
                {
                    reloj = 0;
                    finish.Play();
                    estadoJuegoAnt = 1;
                    estadoJuego = 7;
                    if (golesAzul > golesRojo)
                    {
                        ganador = "Blue";
                        colorGanador = Color.DarkBlue;
                    }
                    else if (golesAzul < golesRojo)
                    {
                        ganador = "Red";
                        colorGanador = Color.Red;
                    }
                    else
                        ganador = "empate";
                }
            }else if (juegoXgoles == true)
            {
                if (golesAzul == numGoles || golesRojo == numGoles)
                {
                    reloj = 0;
                    finish.Play();
                    estadoJuegoAnt = estadoJuego;
                    estadoJuego = 7;
                    if (golesAzul > golesRojo)
                    {
                        ganador = "Blue";
                        colorGanador = Color.DarkBlue;
                    }
                    else if (golesAzul < golesRojo)
                    {
                        ganador = "Red";
                        colorGanador = Color.Red;
                    }
                    else
                        ganador = "empate";
                }
            }

            fms2++;
            fms3++;
            fms4++;
            fms5++;
            fms6++;

            teclado = Keyboard.GetState();

            if (numJugadores == 1)
            {
                if (xBall > xPR && xBall < 500 && yPR > yBall && deltaX < 0)
                    yPR -= 3;
                if (xBall > xPR && xBall < 500 && yPR < yBall && deltaX < 0)
                    yPR += 3;

                if (xBall < xPR && yPR + porteroRojo.Width / 2 < yBall)
                    yPR -= 3;
                if (xBall < xPR && yPR + porteroRojo.Width / 2 > yBall)
                    yPR += 3;
            }
            else if (numJugadores == 2)
            {
                if (teclado.IsKeyDown(Keys.W))
                    yPR -= 3;
                if (teclado.IsKeyDown(Keys.S))
                    yPR += 3;
            }
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

        void drawSoccerPong()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(porteroAzul, new Rectangle(xPA, yPA, porteroAzul.Width, alturaPA), Color.White);
            spriteBatch.Draw(porteroRojo, new Rectangle(xPR, yPR, porteroRojo.Width, alturaPR), Color.White);
            spriteBatch.Draw(balon, new Rectangle(xBall, yBall, sizeX, sizeY), ballColor);  
            
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

            spriteBatch.DrawString(score, golesRojo.ToString(), new Vector2(340, 18), Color.Red);
            spriteBatch.DrawString(score, golesAzul.ToString(), new Vector2(442, 18), Color.DarkBlue);
            spriteBatch.DrawString(timer, mins.ToString("00") + ":" + segs.ToString("00"), new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 23), Color.White);            
        }

        void updEndOfGame()
        {
            mouse = Mouse.GetState();
            teclado = Keyboard.GetState();
            boton1 = new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height);
            boton2 = new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height);
            boton3 = new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height);
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if ((mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed) || (buttonG1Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG1Color = Color.White;
                if (estadoJuegoAnt == 1)
                {
                    initSoccerPong();
                    estadoJuego = 1;
                } else if (estadoJuegoAnt == 4)
                {
                    initSoccerTable();
                    estadoJuego = 4;
                }
            }
            if ((mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed) || (buttonG2Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG2Color = Color.White;
                estadoJuego = 5;                              
            }
            if ((mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed) || (buttonG3Color == Color.CadetBlue && teclado.IsKeyDown(Keys.Enter)))
            {
                buttonG3Color = Color.White;
                fms8 = 0;
                estadoJuegoAnt = estadoJuego;
                estadoJuego = 0;
            }

            if (menuDelay == 0)
            {
                if (teclado.IsKeyDown(Keys.Down))
                {
                    choseKey = true;
                    if (buttonG1Color == Color.CadetBlue)
                    {
                        buttonG1Color = Color.White;
                        buttonG2Color = Color.CadetBlue;
                    }
                    else if (buttonG2Color == Color.CadetBlue)
                    {
                        buttonG2Color = Color.White;
                        buttonG3Color = Color.CadetBlue;
                    }
                    else if (buttonG3Color == Color.CadetBlue)
                    {
                        buttonG3Color = Color.White;
                        buttonG1Color = Color.CadetBlue;
                    }
                    else
                        buttonG1Color = Color.CadetBlue;
                }

                if (teclado.IsKeyDown(Keys.Up))
                {
                    choseKey = true;
                    if (buttonG1Color == Color.CadetBlue)
                    {
                        buttonG1Color = Color.White;
                        buttonG3Color = Color.CadetBlue;
                    }
                    else if (buttonG2Color == Color.CadetBlue)
                    {
                        buttonG2Color = Color.White;
                        buttonG1Color = Color.CadetBlue;
                    }
                    else if (buttonG3Color == Color.CadetBlue)
                    {
                        buttonG3Color = Color.White;
                        buttonG2Color = Color.CadetBlue;
                    }
                    else
                        buttonG3Color = Color.CadetBlue;
                }
            }

            if (choseKey == true)
                menuDelay++;
            if (menuDelay == 15)
            {
                menuDelay = 0;
                choseKey = false;
            }
        }

        void drawEndOfGame()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(400 - buttonG1.Width / 2, 140, buttonG1.Width, buttonG1.Height), buttonG1Color);
            spriteBatch.Draw(buttonG2, new Rectangle(400 - buttonG2.Width / 2, 235, buttonG2.Width, buttonG2.Height), buttonG2Color);
            spriteBatch.Draw(buttonG3, new Rectangle(400 - buttonG3.Width / 2, 330, buttonG3.Width, buttonG3.Height), buttonG3Color);
            if (ganador == "empate")
                spriteBatch.DrawString(titulo, "It's a tie!", new Vector2(275, 70), Color.Purple);
            else
                spriteBatch.DrawString(titulo, "Congrats " + ganador + " Team!", new Vector2(155, 70), colorGanador);
            spriteBatch.DrawString(botones, "Restart", new Vector2(360, 155), Color.Black);
            if (estadoJuegoAnt == 1)
                spriteBatch.DrawString(botones, "SoccerPong", new Vector2(335, 240), Color.Black);
            else if (estadoJuegoAnt == 4)
                spriteBatch.DrawString(botones, "SoccerTable", new Vector2(330, 240), Color.Black);
            spriteBatch.DrawString(botones, "Menu", new Vector2(370, 262), Color.Black);
            spriteBatch.DrawString(botones, "Main Menu", new Vector2(340, 345), Color.Black);
            if (golesRojo < 10)
                spriteBatch.DrawString(marcadorFinal, golesRojo.ToString(), new Vector2(140, 180), Color.Red);
            else
                spriteBatch.DrawString(marcadorFinal2, golesRojo.ToString(), new Vector2(130, 190), Color.Red);
            if (golesAzul < 10)
                spriteBatch.DrawString(marcadorFinal, golesAzul.ToString(), new Vector2(555, 180), Color.DarkBlue);
            else
                spriteBatch.DrawString(marcadorFinal2, golesAzul.ToString(), new Vector2(545, 180), Color.DarkBlue);
        }

        void updEndOfGame2()
        {
            mouse = Mouse.GetState();
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);
            boton1 = new Rectangle(247, 345, buttonInstructions.Width - 15, buttonExit.Height);
            boton2 = new Rectangle(420, 345, buttonInstructions.Width - 15, buttonExit.Height);

            if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed)
            {
                initSoccerFall();
                estadoJuego = 2;
            }
            if (mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed)
            {
                estadoJuegoAnt = 9;
                estadoJuego = 0;
                fms8 = 0;
            }

        }

        void drawEndOfGame2()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.DrawString(titulo, "End of game", new Vector2(255, 70), Color.Blue);
            spriteBatch.DrawString(resultados, "Goals saved: " + salvados, new Vector2(267, 155), Color.Red);
            spriteBatch.DrawString(resultados, "Goals received: " + recibidos, new Vector2(250, 205), Color.Blue);
            spriteBatch.DrawString(resultados, "Playing time: " + mins.ToString("00") + ":" + segs.ToString("00"), new Vector2(243, 265), Color.Red);
            spriteBatch.Draw(buttonG1, new Rectangle(247, 345, buttonInstructions.Width - 15, buttonExit.Height), buttonG1Color);
            spriteBatch.Draw(buttonExit, new Rectangle(420, 345, buttonInstructions.Width - 15, buttonExit.Height), buttonExitColor);
            spriteBatch.DrawString(botones, "Restart", new Vector2(274, 352), Color.Black);
            spriteBatch.DrawString(botones, "Main Menu", new Vector2(428, 352), Color.Black);
        }

        void updGameMenu()
        {
            mouse = Mouse.GetState();
            teclado = Keyboard.GetState();
            botonUnJugador = new Rectangle(375, 155, 115, 35);
            botonDosJugadores = new Rectangle(510, 155, 130, 35);
            botonUnMinuto = new Rectangle(375, 215, 125, 35);
            botonTresMinutos = new Rectangle(510, 215, 125, 35);
            botonCincoGoles = new Rectangle(422, 275, 35, 35);
            botonSieteGoles = new Rectangle(491, 275, 35, 35);
            botonDiezGoles = new Rectangle(562, 275, 35, 35);
            boton1 = new Rectangle(299, 345, buttonExit.Width, buttonExit.Height);
            boton2 = new Rectangle(426, 345, buttonExit.Width, buttonExit.Height);
            boton3 = new Rectangle(535, 370, buttonInstructions.Width, buttonInstructions.Height);
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);
            
            if (mouseObj.Intersects(botonUnJugador) && mouse.LeftButton == ButtonState.Pressed)
            {
                numJugadores = 1;
                if (juegoXtiempo == false && juegoXgoles == false)
                    opcionesEscogidas = 1;
                else
                    opcionesEscogidas = 2;
            }
            if (mouseObj.Intersects(botonDosJugadores) && mouse.LeftButton == ButtonState.Pressed)
            {
                numJugadores = 2;
                if (opcionesEscogidas == 0)
                    opcionesEscogidas = 1;
                else if (opcionesEscogidas == 1)
                    opcionesEscogidas = 2;
            }
            if (mouseObj.Intersects(botonUnMinuto) && mouse.LeftButton == ButtonState.Pressed)
            {
                tiempoPartido = 1;
                if (numJugadores == 0)
                    opcionesEscogidas = 1;
                else 
                    opcionesEscogidas = 2;
                juegoXtiempo = true;
                juegoXgoles = false;
            }
            else if (mouseObj.Intersects(botonTresMinutos) && mouse.LeftButton == ButtonState.Pressed)
            {
                tiempoPartido = 3;
                if (numJugadores == 0)
                    opcionesEscogidas = 1;
                else 
                    opcionesEscogidas = 2;
                juegoXtiempo = true;
                juegoXgoles = false;
            }           
            if (mouseObj.Intersects(botonCincoGoles) && mouse.LeftButton == ButtonState.Pressed)
            {
                numGoles = 5;
                if (numJugadores == 0)
                    opcionesEscogidas = 1;
                else
                    opcionesEscogidas = 2;
                juegoXtiempo = false;
                juegoXgoles = true;
            }
            else if (mouseObj.Intersects(botonSieteGoles) && mouse.LeftButton == ButtonState.Pressed)
            {
                numGoles = 7;
                if (numJugadores == 0)
                    opcionesEscogidas = 1;
                else
                    opcionesEscogidas = 2;
                juegoXtiempo = false;
                juegoXgoles = true;
            }
            else if (mouseObj.Intersects(botonDiezGoles) && mouse.LeftButton == ButtonState.Pressed)
            {
                numGoles = 10;
                if (numJugadores == 0)
                    opcionesEscogidas = 1;
                else
                    opcionesEscogidas = 2;
                juegoXtiempo = false;
                juegoXgoles = true;
            }

            if (opcionesEscogidas == 2)
            {
                buttonG1Color = Color.White;
                if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed && fms8 >= 10)
                {
                    fms8 = 0;
                    if (game == "SoccerPong")
                    {
                        initSoccerPong();
                        estadoJuego = 1;
                    }else if (game == "SoccerTable")
                    {
                        initSoccerTable();
                        estadoJuego = 4;
                    }else if (game == "SoccerFall")
                    {
                        initSoccerFall();
                        estadoJuego = 2;
                    }
                }
            }
            else
                buttonG1Color = Color.Gray;
            fms8++;
            if (mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed && fms8 >= 10)
            {
                estadoJuegoAnt = 5;
                estadoJuego = 0;
                fms8 = 0;
                buttonG1Color = Color.White;
            }
            if (mouseObj.Intersects(boton3) && mouse.LeftButton == ButtonState.Pressed)
            {
                estadoJuegoAnt = 5;
                estadoJuego = 8;
                fms8 = 0;
            }
            
        }

        void drawGameMenu()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 60, menuRect.Width, menuRect.Height), Color.White);
            spriteBatch.Draw(buttonG1, new Rectangle(299, 345, buttonExit.Width, buttonExit.Height), buttonG1Color);
            spriteBatch.Draw(buttonExit, new Rectangle(426, 345, buttonExit.Width, buttonExit.Height), buttonExitColor);
            spriteBatch.Draw(buttonInstructions, new Rectangle(535, 370, buttonInstructions.Width, buttonInstructions.Height), buttonInstructionsColor);
            if (game == "SoccerPong")
                spriteBatch.DrawString(titulo, "SoccerPong", new Vector2(256, 70), Color.Blue);
            else if (game == "SoccerTable")
                spriteBatch.DrawString(titulo, "SoccerTable", new Vector2(250, 70), Color.Blue);
            else if (game == "SoccerFall")
                spriteBatch.DrawString(titulo, "SoccerFall", new Vector2(256, 70), Color.Blue);
            
                spriteBatch.DrawString(botones, "Start", new Vector2(309, 350), Color.Black);
                spriteBatch.DrawString(botones, "Back", new Vector2(439, 350), Color.Black);
                spriteBatch.DrawString(botones, "Instructions", new Vector2(545, 375), Color.Black);
                spriteBatch.DrawString(botones, "# of Players", new Vector2(166, 160), Color.White);
                spriteBatch.DrawString(botones, "1 player   2 players", new Vector2(386, 160), Color.White);
                spriteBatch.DrawString(botones, "Time", new Vector2(216, 220), Color.White);
                spriteBatch.DrawString(botones, "1:30 mins  3:00 mins", new Vector2(386, 220), Color.White);
                spriteBatch.DrawString(botones, "# of goals", new Vector2(178, 280), Color.White);
                spriteBatch.DrawString(botones, "5    7    10", new Vector2(432, 280), Color.White);
            

            if (numJugadores == 1)
                spriteBatch.Draw(botonVacio, new Rectangle(375, 155, 115, 35), Color.White);
            else if (numJugadores == 2)
                spriteBatch.Draw(botonVacio, new Rectangle(510, 155, 130, 35), Color.White);
            if (juegoXtiempo == true)
            {                
                if (tiempoPartido == 1)
                    spriteBatch.Draw(botonVacio, new Rectangle(375, 215, 125, 35), Color.White);
                else if (tiempoPartido == 3)
                    spriteBatch.Draw(botonVacio, new Rectangle(510, 215, 125, 35), Color.White);
            }

            if (juegoXgoles == true)
            {                
                if (numGoles == 5)
                    spriteBatch.Draw(botonVacio, new Rectangle(422, 275, 35, 35), Color.White);
                else if (numGoles == 7)
                    spriteBatch.Draw(botonVacio, new Rectangle(491, 275, 35, 35), Color.White);
                else if (numGoles == 10)
                    spriteBatch.Draw(botonVacio, new Rectangle(562, 275, 35, 35), Color.White);
            }
        }
        void updInstructions()
        {
            mouse = Mouse.GetState();            
            mouseObj = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (game == "SoccerFall")
            {
                boton2 = new Rectangle(426, 370, buttonExit.Width, buttonExit.Height);
                boton1 = new Rectangle(299, 370, buttonExit.Width, buttonExit.Height);
                if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed)
                {
                    estadoJuegoAnt = 8;
                    initSoccerFall();
                    estadoJuego = 2;
                }
                if (mouseObj.Intersects(boton2) && mouse.LeftButton == ButtonState.Pressed)
                {
                    estadoJuegoAnt = 8;
                    estadoJuego = 0;
                    fms8 = 0;
                }
            }else
            {
                boton1 = new Rectangle(360, 380, buttonExit.Width, buttonExit.Height);
                if (mouseObj.Intersects(boton1) && mouse.LeftButton == ButtonState.Pressed)
                {
                    estadoJuegoAnt = 8;
                    estadoJuego = 5;
                    fms8 = 0;
                }
            }

        }

        void drawInstructions()
        {
            spriteBatch.Draw(menuRect, new Rectangle(100, 30, menuRect.Width, menuRect.Height + 40), Color.White);
            if (game == "SoccerFall")
            {
                spriteBatch.Draw(buttonG1, new Rectangle(299, 370, buttonExit.Width, buttonExit.Height), buttonG1Color);
                spriteBatch.Draw(buttonExit, new Rectangle(426, 370, buttonExit.Width, buttonExit.Height), buttonExitColor);
                spriteBatch.DrawString(titulo, "SoccerFall", new Vector2(270, 35), Color.Blue);
                spriteBatch.Draw(instruccionesSFall, new Rectangle(145, 95, 500, 260), Color.White);
                spriteBatch.DrawString(botones, "Start", new Vector2(309, 375), Color.Black);
                spriteBatch.DrawString(botones, "Back", new Vector2(439, 375), Color.Black);
            } else {
                spriteBatch.Draw(buttonExit, new Rectangle(360, 380, buttonExit.Width, buttonExit.Height), buttonExitColor);

                if (game == "SoccerPong")
                {
                    spriteBatch.DrawString(titulo, "SoccerPong", new Vector2(256, 35), Color.Blue);
                    spriteBatch.Draw(instruccionesSPong, new Rectangle(145, 105, 520, 275), Color.White);
                }
                else if (game == "SoccerTable")
                {
                    spriteBatch.DrawString(titulo, "SoccerTable", new Vector2(250, 35), Color.Blue);
                    spriteBatch.Draw(instruccionesSTable, new Rectangle(145, 95, 500, 260), Color.White);
                }
                spriteBatch.DrawString(botones, "Back", new Vector2(373, 385), Color.Black);
            }              
        }

        void initSoccerFall()
        {
            start.Play();
            xPosPlayer = graphics.GraphicsDevice.Viewport.Width / 2 - 20;
            yPosPlayer = 440;

            selectImg = rnd.Next(1, 4);
            vX = 0;
            vY = rnd.Next(2, 4);
            pX = rnd.Next(50, 700);
            pY = rnd.Next(250) * -1;
            salvados = 0;
            recibidos = 0;
            vidas = 6;
            reloj = 0;
            mins = 0;
            segs = 0;
            marca = false;
            listaObjsBuenos.Clear();
            listaObjsMalos.Clear();
        }

        void updSoccerFall()
        {
            reloj++;

            if (reloj == 60)
            {
                segs++;
                reloj = 0;
            }
            if (segs > 59)
            {
                mins++;
                segs = 0;
            }

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

            time++;
            Rectangle areaJugador = new Rectangle(xPosPlayer, yPosPlayer, player.Width, player.Height);

            foreach (ObjetosADibujar obj in listaObjsBuenos)
            {
                obj.PosicY += obj.VelY;
                obj.PosicX += obj.VelX;
                
                Rectangle areaObjetoBueno = new Rectangle(obj.PosicX, obj.PosicY, obj.Imagen.Width, obj.Imagen.Height);

                if (obj.PosicY >= 480 && obj.PosicY < 482)
                    recibidos++;

                if (areaJugador.Intersects(areaObjetoBueno))
                {
                    obj.VelY *= -1;
                    obj.VelX = rnd.Next(-2, 3);
                    salvados++;
                }
                if (salvados % 15 != 0)
                    reducir = true;

                if (salvados % 15 == 0 && reducir == true)
                {
                    reducir = false;
                    vidas++;
                }
                
                if (vidas <= 0)
                {
                    estadoJuegoAnt = 2;
                    estadoJuego = 9;
                    finish.Play();
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
                    vidas--;
                    smack.Play();
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
            vY = rnd.Next(2, 4);
            pX = rnd.Next(100, 700);
            pY = rnd.Next(250) * -1;
        }

        void drawSoccerFall()
        {
            spriteBatch.Draw(background2, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(player, new Rectangle(xPosPlayer, yPosPlayer, player.Width, player.Height), Color.White);
            spriteBatch.DrawString(letrero, "Goals saved " + salvados, new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(letrero, "Goals received " + recibidos, new Vector2(20, 45), Color.White);
            spriteBatch.DrawString(letrero, "Lives " + vidas, new Vector2(685, 25), Color.White);
            spriteBatch.DrawString(timer, mins.ToString("00") + ":" + segs.ToString("00"), new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 23), Color.White);

            foreach (ObjetosADibujar obj in listaObjsBuenos) {
                spriteBatch.Draw(obj.Imagen, new Rectangle(obj.PosicX, obj.PosicY, obj.Imagen.Width, obj.Imagen.Height), Color.White);                
            }

           // fms7++;
           // if (fms7 == 8)
           // {
           //     fms7 = 0;
           // }

            foreach (ObjetosADibujar obj in listaObjsMalos)
            {
                fms7++;
                if (fms7 == 8)
                {
                    obj.Actual++;
                    fms7 = 0;
                    if (obj.Actual >= 8)
                        obj.Actual = 0;
                }                
                spriteBatch.Draw(obj.Imagenes[obj.Actual], new Rectangle(obj.PosicX, obj.PosicY, obj.Imagenes[0].Width, obj.Imagenes[0].Height), Color.White);
                
            }
        }

        void initSoccerTable()
        {
            start.Play();
            golAzul = false;
            golRojo = false;
            tiempoEsperaGol = 3;
            esperaGol = 0;

            xEA = 242;
            yEA = -217;
            xER = 55;
            yER = -217;

            xBall = graphics.GraphicsDevice.Viewport.Width / 2 - 20;
            yBall = -50;

            do
            {
                deltaX = rnd.Next(-1, 2);
            } while (deltaX == 0);
                        
            deltaY = rnd.Next(-7,-5);            

            fms3 = 610;
            fms4 = 610;
            fms5 = 610;
            tiempoPatadaRoja = 0;
            tiempoPatadaAzul = 0;
            reloj = 0;
            if (juegoXtiempo == true)
            {
                if (tiempoPartido == 1)
                {
                    mins = 1;
                    segs = 30;
                }else if (tiempoPartido == 3)
                {
                    mins = 3;
                    segs = 0;
                }
            }
            else
            {
                mins = 0;
                segs = 0;
            }
            golesRojo = 0;
            golesAzul = 0;

            yRojo1 = 230;
            yRojo2 = 108;
            yRojo3 = 230;
            yRojo4 = 350;
            yRojo5 = 106;
            yRojo6 = 227;
            yRojo7 = 352;
            yRojo8 = 160;
            yRojo9 = 298;
            yAzul1 = 235;
            yAzul2 = 125;
            yAzul3 = 235;
            yAzul4 = 362;
            yAzul5 = 120;
            yAzul6 = 235;
            yAzul7 = 362;
            yAzul8 = 183;
            yAzul9 = 317;            
        }

        void updSoccerTable()
        {
            bal = new Rectangle(xBall, yBall, balonChico.Width, balonChico.Height);

            rojo1 = new Rectangle(90, yRojo1, jugador.Width, jugador.Height);
            rojo2 = new Rectangle(187, yRojo2, jugador.Width, jugador.Height);
            rojo3 = new Rectangle(187, yRojo3, jugador.Width, jugador.Height);
            rojo4 = new Rectangle(188, yRojo4, jugador.Width, jugador.Height);
            rojo5 = new Rectangle(347, yRojo5, jugador.Width, jugador.Height);
            rojo6 = new Rectangle(345, yRojo6, jugador.Width, jugador.Height);
            rojo7 = new Rectangle(345, yRojo7, jugador.Width, jugador.Height);
            rojo8 = new Rectangle(553, yRojo8, jugador.Width, jugador.Height);
            rojo9 = new Rectangle(553, yRojo9, jugador.Width, jugador.Height);
            azul1 = new Rectangle(690, yAzul1, jugador.Width, jugador.Height);
            azul2 = new Rectangle(594, yAzul2, jugador.Width, jugador.Height);
            azul3 = new Rectangle(594, yAzul3, jugador.Width, jugador.Height);
            azul4 = new Rectangle(594, yAzul4, jugador.Width, jugador.Height);
            azul5 = new Rectangle(435, yAzul5, jugador.Width, jugador.Height);
            azul6 = new Rectangle(435, yAzul6, jugador.Width, jugador.Height);
            azul7 = new Rectangle(435, yAzul7, jugador.Width, jugador.Height);
            azul8 = new Rectangle(250, yAzul8, jugador.Width, jugador.Height);
            azul9 = new Rectangle(250, yAzul9, jugador.Width, jugador.Height);

            teclado = Keyboard.GetState();

            if (teclado.IsKeyDown(Keys.Down))
            {
                yEA += 4;
                yAzul1 += 4;
                yAzul2 += 4;
                yAzul3 += 4;
                yAzul4 += 4;
                yAzul5 += 4;
                yAzul6 += 4;
                yAzul7 += 4;
                yAzul8 += 4;
                yAzul9 += 4;
            }
            if (teclado.IsKeyDown(Keys.Up))
            {
                yEA -= 4;
                yAzul1 -= 4;
                yAzul2 -= 4;
                yAzul3 -= 4;
                yAzul4 -= 4;
                yAzul5 -= 4;
                yAzul6 -= 4;
                yAzul7 -= 4;
                yAzul8 -= 4;
                yAzul9 -= 4;
            }
            if (kickDelayAzul == 0)
            {
                if (teclado.IsKeyDown(Keys.Space))
                {
                    patadaAz = true;
                    patadaAzul = true;
                    azulActual = 1;
                }
            }
            if (numJugadores == 2)
            {
                if (teclado.IsKeyDown(Keys.W))
                {
                    yER -= 4;
                    yRojo1 -= 4;
                    yRojo2 -= 4;
                    yRojo3 -= 4;
                    yRojo4 -= 4;
                    yRojo5 -= 4;
                    yRojo6 -= 4;
                    yRojo7 -= 4;
                    yRojo8 -= 4;
                    yRojo9 -= 4;
                }
                if (teclado.IsKeyDown(Keys.S))
                {
                    yER += 4;
                    yRojo1 += 4;
                    yRojo2 += 4;
                    yRojo3 += 4;
                    yRojo4 += 4;
                    yRojo5 += 4;
                    yRojo6 += 4;
                    yRojo7 += 4;
                    yRojo8 += 4;
                    yRojo9 += 4;
                }
                if (kickDelayRojo == 0)
                {
                    if (teclado.IsKeyDown(Keys.F))
                    {
                        patadaRj = true;
                        patadaRoja = true;
                        rojoActual = 1;
                    }
                }
            } else if (numJugadores == 1)
            {
               // if (yBall < yRojo8 + 60)   
                if (yBall < yRojo8 - 150)
                {
                    yER -= 4;
                    yRojo1 -= 4;
                    yRojo2 -= 4;
                    yRojo3 -= 4;
                    yRojo4 -= 4;
                    yRojo5 -= 4;
                    yRojo6 -= 4;
                    yRojo7 -= 4;
                    yRojo8 -= 4;
                    yRojo9 -= 4;
                }
               // if (yBall > yRojo8 + 80)   
                if (yBall > yRojo8 + 250)
                {
                    yER += 4;
                    yRojo1 += 4;
                    yRojo2 += 4;
                    yRojo3 += 4;
                    yRojo4 += 4;
                    yRojo5 += 4;
                    yRojo6 += 4;
                    yRojo7 += 4;
                    yRojo8 += 4;
                    yRojo9 += 4;
                }
                if (kickDelayRojo == 0)
                {
                    if (rnd2.Next(0, 5) < 3)
                    {
                        if (bal.Intersects(rojo1))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo2))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo3))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo4))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo5))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo6))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo7))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo8))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                        else if (bal.Intersects(rojo9))
                        {
                            patadaRj = true;
                            patadaRoja = true;
                            rojoActual = 1;
                        }
                    }
                }
            }            

            if (patadaAz == true)
                kickDelayAzul++;
            if (kickDelayAzul == 20)
            {
                patadaAz = false;
                kickDelayAzul = 0;
            }
            if (patadaRj == true)
                kickDelayRojo++;
            if (kickDelayRojo == 30)
            {
                patadaRj = false;
                kickDelayRojo = 0;
            }

            if (patadaAzul == true)
            {
                tiempoPatadaAzul++;   
                if (bal.Intersects(azul1))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul2))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul3))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul4))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul5))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul6))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul7))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul8))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(azul9))
                {
                    if (deltaX < 0)
                        deltaX -= 1;
                    if (deltaX > 0)
                        deltaX *= -1;
                }               
            }

            if (patadaRoja == true)
            {
                tiempoPatadaRoja++;
                if (bal.Intersects(rojo1))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo2))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo3))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo4))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo5))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo6))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo7))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo8))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
                if (bal.Intersects(rojo9))
                {
                    if (deltaX > 0)
                        deltaX += 1;
                    if (deltaX < 0)
                        deltaX *= -1;
                }
            }

            if (tiempoPatadaAzul == 10)
            {
                patadaAzul = false;
                azulActual = 0;
                tiempoPatadaAzul = 0;
            }

            if (tiempoPatadaRoja == 10)
            {
                patadaRoja = false;
                rojoActual = 0;
                tiempoPatadaRoja = 0;
            }              

            if (yEA < -310)
                yEA = -310;
            if (yEA > -140)
                yEA = -140;
            if (yER < -302)
                yER = -302;
            if (yER > -135)
                yER = -135;

            //Limita el movimiento de los rectangulos a la posición de los jugadores
            if (yAzul1 < 142)
                yAzul1 = 142;
            if (yAzul1 > 312)
                yAzul1 = 312;
            if (yAzul2 < 32)
                yAzul2 = 32;
            if (yAzul2 > 202)
                yAzul2 = 202;
            if (yAzul3 < 142)
                yAzul3 = 142;
            if (yAzul3 > 312)
                yAzul3 = 312;
            if (yAzul4 < 269)
                yAzul4 = 269;
            if (yAzul4 > 439)
                yAzul4 = 439;
            if (yAzul5 < 27)
                yAzul5 = 27;
            if (yAzul5 > 197)
                yAzul5 = 197;
            if (yAzul6 < 142)
                yAzul6 = 142;
            if (yAzul6 > 312)
                yAzul6 = 312;
            if (yAzul7 < 269)
                yAzul7 = 269;
            if (yAzul7 > 439)
                yAzul7 = 439;
            if (yAzul8 < 90)
                yAzul8 = 90;
            if (yAzul8 > 260)
                yAzul8 = 260;
            if (yAzul9 < 224)
                yAzul9 = 224;
            if (yAzul9 > 394)
                yAzul9 = 394;
            if (yRojo1 < 145)
                yRojo1 = 145;
            if (yRojo1 > 312)
                yRojo1 = 312;
            if (yRojo2 < 23)
                yRojo2 = 23;
            if (yRojo2 > 190)
                yRojo2 = 190;
            if (yRojo3 < 145)
                yRojo3 = 145;
            if (yRojo3 > 312)
                yRojo3 = 312;
            if (yRojo4 < 265)
                yRojo4 = 265;
            if (yRojo4 > 432)
                yRojo4 = 432;
            if (yRojo5 < 21)
                yRojo5 = 21;
            if (yRojo5 > 188)
                yRojo5 = 188;
            if (yRojo6 < 142)
                yRojo6 = 142;
            if (yRojo6 > 309)
                yRojo6 = 309;
            if (yRojo7 < 267)
                yRojo7 = 267;
            if (yRojo7 > 434)
                yRojo7 = 434;
            if (yRojo8 < 75)
                yRojo8 = 75;
            if (yRojo8 > 242)
                yRojo8 = 242;
            if (yRojo9 < 213)
                yRojo9 = 213;
            if (yRojo9 > 380)
                yRojo9 = 380;
            
            if (xBall > (graphics.GraphicsDevice.Viewport.Width - balonChico.Width))
                deltaX = -6;
            if (xBall < 0)
                deltaX = 6;

            if (yBall > (graphics.GraphicsDevice.Viewport.Height - balonChico.Height) || yBall < 0)
                deltaY *= -1;

            if (xBall < 0 && (yBall > 150 && yBall < 250))
            {
                goal.Play();
                golAzul = true;
                golesAzul++;
                xBall = (graphics.GraphicsDevice.Viewport.Width / 2) - (balonChico.Width / 2);
                yBall = -150;
                deltaX = 0;
                deltaY = 0;                
            }

            if (xBall > (graphics.GraphicsDevice.Viewport.Width - balonChico.Width) && (yBall > 150 && yBall < 250))
            {
                goal.Play();
                golRojo = true;
                golesRojo++;
                xBall = (graphics.GraphicsDevice.Viewport.Width / 2) - (balonChico.Width / 2);
                yBall = -150;
                deltaY = 0;
                deltaX = 0;                
            }

            if (golAzul == true || golRojo == true) {
                esperaGol++;
                if (esperaGol == 60)
                {
                    tiempoEsperaGol -= 1;
                    esperaGol = 0;
                }
                if (tiempoEsperaGol == 0)
                {
                    if (golAzul == true)
                    {
                        golAzul = false;
                        deltaX = -1;
                    }else if (golRojo == true)
                    {
                        golRojo = false;
                        deltaX = 1;
                    }
                    tiempoEsperaGol = 3;
                    deltaY = rnd.Next(4, 7);
                }
            }                

            if (xBall > (graphics.GraphicsDevice.Viewport.Width - balonChico.Width))
                xBall = graphics.GraphicsDevice.Viewport.Width - balonChico.Width;
            if (xBall < 0)
                xBall = 0;
            if (yBall > graphics.GraphicsDevice.Viewport.Height - balonChico.Height)
                yBall = graphics.GraphicsDevice.Viewport.Height - balonChico.Height;
            if (yBall < 0)
                yBall = 0;

            xBall += deltaX;
            yBall += deltaY;

            if (golAzul == false && golRojo == false)
                reloj++;
            if (reloj == 60)
            {
                if (juegoXtiempo == true)
                    segs--;
                else if (juegoXgoles == true)
                    segs++;
                reloj = 0;
            }

            if (segs == 60)
            {
                mins++;                
                segs = 0;
            }
            if (segs < 0)
            {
                mins--;
                segs = 59;
            }

            if (juegoXtiempo == true)
            {
                if (mins == 0 && segs == 0)
                {
                    reloj = 0;
                    finish.Play();
                    estadoJuegoAnt = 4;
                    estadoJuego = 7;
                    if (golesAzul > golesRojo)
                    {
                        ganador = "Blue";
                        colorGanador = Color.DarkBlue;
                    }
                    else if (golesAzul < golesRojo)
                    {
                        ganador = "Red";
                        colorGanador = Color.Red;
                    }
                    else
                        ganador = "empate";
                }
            }
            else if (juegoXgoles == true)
            {
                if (golesAzul == numGoles || golesRojo == numGoles)
                {
                    reloj = 0;
                    finish.Play();
                    estadoJuegoAnt = 4;
                    estadoJuego = 7;
                    if (golesAzul > golesRojo)
                    {
                        ganador = "Blue";
                        colorGanador = Color.DarkBlue;
                    }
                    else if (golesAzul < golesRojo)
                    {
                        ganador = "Red";
                        colorGanador = Color.Red;
                    }
                    else
                        ganador = "empate";
                }
            }
        }

        void drawSoccerTable()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(balonChico, new Rectangle(xBall, yBall, balonChico.Width, balonChico.Height), Color.White);
            spriteBatch.Draw(equipoRojo[rojoActual], new Rectangle(xER, yER, equipoRojo[0].Width, equipoRojo[0].Height), Color.White);
            spriteBatch.Draw(equipoAzul[azulActual], new Rectangle(xEA, yEA, equipoAzul[0].Width, equipoAzul[0].Height), Color.White);
            spriteBatch.DrawString(score, golesRojo.ToString(), new Vector2(340, 18), Color.Red);
            spriteBatch.DrawString(score, golesAzul.ToString(), new Vector2(442, 18), Color.DarkBlue);
            if (golAzul == true || golRojo == true)
            {
                spriteBatch.DrawString(relojEspera, tiempoEsperaGol.ToString(), new Vector2(377, 190), Color.White);
            }

            spriteBatch.DrawString(timer, mins.ToString("00") + ":" + segs.ToString("00"), new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 23), Color.White);
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
