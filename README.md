# Projeto 2 LP1 - Snakes & Ladders

## Autoria

### João Borges - a21904214
> Responsável por gerar o tabuleiro e *core loop* do jogo, e pelas ações especiais do tabuleiro (Snakes, Ladders, etc..).

### João Matos - a21901219
> Responsável pela estruturação do projeto, representação do menu e tabela do jogo, movimento do jogador e *game loop*.

### Rodrigo Marques - a21802593
> Responsável pela estrutura dos dados do jogo, representação dos jogadores e descrição das casas do jogo, restrição da posição do jogador no tabuleiro e colisão.

## Arquitetura da solução
Para a solução do nosso projeto, utilizamos o *design pattern* `MVC`, de forma a manter o projeto organizado em dados, lógica e visualização separados, não deixando o funcionamento lógico do projeto interferir com a sua representação e vice-versa.

Para os dados foram utilizados uma enumeração `Space`, que define os diferentes tipos de casa no tabuleiro, uma classe `Player` que guarda a aparência do jogador e a sua posição, utilizando propriedades para aceder às mesmas, e uma classe `Table` que vai conter um *array* bidimensional do tipo `Space`, para guardar o tabuleiro criado no início do jogo.

Além desta enumeração e classes responsáveis por guardar os dados do jogo, temos também uma classe `MainController` que serve de controlador, que contém toda a lógica do jogo, e uma classe `MainView`, que se encarrega de representar todos os dados na consola e ler o *input* do utilizador.

Quando corremos o programa, os dados do tabuleiro (`Table`) são criados recebendo o seu tamanho. Quando criado o controlador `MainController`, recebe a `Table` já criada, correndo depois o método `GameLoop()` que dá início ao *core loop* do jogo e recebendo como parâmetro a `View` previamente criada.

No *core loop*, temos um *loop* para as opções do menu onde o jogador deve escolher a opção que deseja: Jogar, ler as regras ou sair.

Começando o jogo, geramos o tabuleiro ao preencher o objeto `Table` (previamente criado e guardado no `MainController`) com casas normais, e ao gerar casas especiais com as suas devidas regras de quantidade mínima, quantidade máxima, fila mínima e fila máxima.

Após termos o tabuleiro do jogo, entramos no *game loop*, onde nos é apresentado o tabuleiro ao início de cada jogada, e o jogador deve dar *input* para gerar um número aleatório que simula um dado, que depois fará o jogador avançar esse número de casas, ao ajustar a sua posição vertical e horizontal, garantindo que se encontra dentro dos limites do tabuleiro e aplicando a respetiva ação, ou ações em cadeia, caso seja o caso.

Após o movimento do jogador, verificamos se chegou ao fim do tabuleiro. Se sim, ele ganha o jogo, se não, o seu turno acaba e muda para o outro jogador (que deve agora dar *input*, repetindo este ciclo).  

## Referências
- Aulas de Linguagens de Programação 1 (slides).