import socket
import time

HOST = '127.0.0.1'  # dirección IP del servidor
PORT = 8090        # puerto del servidor


# crea un socket TCP/IP
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    # conecta el socket al servidor
    s.connect((HOST, PORT))
    buffer = ""
    # envía mensajes durante 10 segundos
    while True:
        # Obtener la dirección IP y el puerto del socket
        ip, port = s.getsockname()
        data = input("===> ")

        if data == "":
            res = s.recv(1024)
            if len(res) > 0:
                buffer = res.decode()
                print(buffer)
            continue

        if data == "exit":
            break

        # Convertir a string
        ip_str = str(ip)
        port_str = str(port)
        s.sendall(f"User{port} Says: {data}\n".encode())
        time.sleep(1)
        
    # cierra la conexión
    s.close()
