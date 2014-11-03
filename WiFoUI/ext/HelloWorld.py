import wifo

def displayname():
  return 'Python Test'

def author():
  return 'Hessan Feghhi'

def perform():
  # Display a warning message
  if not wifo.confirm("Do you want to see the magic?"):
    return;

  # Ask a number
  testnum = wifo.askint("Enter a test number")

  if not testnum:
    return

  # Test 1: Display some numbers using propbox
  
  if testnum == 1:
    results = {}
    l = len(wifo.data)
    results['Record Count'] = l
    results['Start Time'] = wifo.data[0][0]
    results['End Time'] = wifo.data[l - 1][0]
    wifo.dictbox(results)

  # Test 2: Display a chart using barplot

  elif testnum == 2:
    xs = [x for x in range(1, 10)]
    ys = [x + 5.2 for x in xs]
    wifo.barplot("Simple Plot", xs, ys)

  # Test 3: Open a file and show its text

  elif testnum == 3:
    fn = wifo.askfile("Choose a file")
    f = open(fn, 'r')
    s = fn.read()
    f.close()
    wifo.message(s)
    
  # Test 4: Ask the user's name and say hello.
  else:
    s = wifo.ask("Enter your name")
    if not s:
      print 'Hello, world!'
    else: wifo.message('Hello, %s!' % s)
