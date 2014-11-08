class Record(object):
  def __init__(self, t, s):
    self.time = t
    self.state = s
  def isfreechan(self):
    return (self.state & 2) == 2

def nextchange(records, startindex, mask):
  if startindex < 0:
    return -1

  comp = records[startindex].state & mask

  for i in range(startindex + 1, len(records)):
    if comp != (records[i].state & mask):
      return i

  return -1