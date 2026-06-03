<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@tabler/icons-webfont@latest/tabler-icons.min.css" />
    <link href="https://fonts.googleapis.com/css2?family=DM+Sans:wght@400;500;600&family=DM+Serif+Display&display=swap" rel="stylesheet" />
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: #f4f4f0;
            font-family: 'DM Sans', sans-serif;
        }

        .login-card {
            background: #ffffff;
            border: 1px solid #e5e5e0;
            border-radius: 20px;
            padding: 2.5rem 2.25rem 2rem;
            width: 100%;
            max-width: 400px;
        }

        .login-brand {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 2rem;
        }

        .login-brand-icon {
            width: 40px;
            height: 40px;
            background: #0F6E56;
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

            .login-brand-icon svg {
                width: 22px;
                height: 22px;
                fill: none;
                stroke: #fff;
                stroke-width: 2;
                stroke-linecap: round;
                stroke-linejoin: round;
            }

        .login-brand-name {
            font-family: 'DM Serif Display', serif;
            font-size: 17px;
            color: #1a1a1a;
            line-height: 1.1;
            display: block;
        }

        .login-brand-sub {
            font-size: 11px;
            color: #888;
            letter-spacing: 0.04em;
            text-transform: uppercase;
            display: block;
        }

        .login-heading {
            font-size: 22px;
            font-weight: 600;
            color: #1a1a1a;
            margin-bottom: 0.25rem;
        }

        .login-subheading {
            font-size: 14px;
            color: #666;
            margin-bottom: 1.75rem;
        }

        .field-group {
            margin-bottom: 1.1rem;
        }

        .field-label {
            display: block;
            font-size: 13px;
            font-weight: 500;
            color: #555;
            margin-bottom: 6px;
        }

        .field-input-wrap {
            position: relative;
        }

            .field-input-wrap i {
                position: absolute;
                left: 12px;
                top: 50%;
                transform: translateY(-50%);
                font-size: 17px;
                color: #888;
                pointer-events: none;
            }

            .field-input-wrap input,
            .field-input-wrap .asp-textbox {
                width: 100%;
                height: 42px;
                padding: 0 12px 0 38px;
                font-size: 14px;
                font-family: 'DM Sans', sans-serif;
                background: #f8f8f6;
                border: 1px solid #e0e0da;
                border-radius: 8px;
                color: #1a1a1a;
                outline: none;
                transition: border-color 0.15s, box-shadow 0.15s;
            }

                .field-input-wrap input:focus,
                .field-input-wrap .asp-textbox:focus {
                    border-color: #0F6E56;
                    box-shadow: 0 0 0 3px rgba(15, 110, 86, 0.12);
                    background: #fff;
                }

        .forgot-row {
            display: flex;
            justify-content: flex-end;
            margin-top: 5px;
        }

        .forgot-link {
            font-size: 12px;
            color: #0F6E56;
            text-decoration: none;
            cursor: pointer;
        }

            .forgot-link:hover {
                text-decoration: underline;
            }

        .btn-ingresar {
            width: 100%;
            height: 44px;
            margin-top: 1.5rem;
            background: #0F6E56;
            color: #fff;
            font-family: 'DM Sans', sans-serif;
            font-size: 15px;
            font-weight: 500;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            transition: background 0.15s, transform 0.1s;
        }

            .btn-ingresar:hover {
                background: #085041;
            }

            .btn-ingresar:active {
                transform: scale(0.98);
            }

        .divider {
            display: flex;
            align-items: center;
            gap: 10px;
            margin: 1.5rem 0 0;
        }

        .divider-line {
            flex: 1;
            height: 1px;
            background: #e5e5e0;
        }

        .divider-text {
            font-size: 12px;
            color: #aaa;
        }

        .login-footer {
            margin-top: 1.5rem;
            text-align: center;
            font-size: 12px;
            color: #aaa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">

            <div class="login-brand">
                <div class="login-brand-icon">
                    <svg viewBox="0 0 24 24">
                        <path d="M12 2L2 7l10 5 10-5-10-5z" />
                        <path d="M2 17l10 5 10-5" />
                        <path d="M2 12l10 5 10-5" />
                    </svg>
                </div>
                <div>
                    <span class="login-brand-name">PlanMejoramiento</span>
                    <span class="login-brand-sub">Gestión educativa</span>
                </div>
            </div>

            <p class="login-heading">Bienvenido</p>
            <p class="login-subheading">Ingresa tus credenciales para continuar</p>

            <div class="field-group">
                <label class="field-label">Correo electrónico</label>
                <div class="field-input-wrap">
                    <i class="ti ti-mail"></i>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="asp-textbox" placeholder="tu@correo.com"></asp:TextBox>
                </div>
            </div>

            <div class="field-group">
                <label class="field-label">Contraseña</label>
                <div class="field-input-wrap">
                    <i class="ti ti-lock"></i>
                    <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="asp-textbox" placeholder="••••••••"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="btn-ingresar" />

            <div class="divider">
                <div class="divider-line"></div>
                <span class="divider-text">sistema institucional</span>
                <div class="divider-line"></div>
            </div>

        </div>
    </form>
</body>
</html>
